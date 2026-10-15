using DG.Tweening;
using Game.Scripts.UI.Animation;
using Game.Scripts.UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class Wheel : Window
    {
        [Header("Выбор лутбокса")]
        [SerializeField] private RectTransform _boxContent;
        [SerializeField] private LootBoxBar _boxBarPrefab;
        [SerializeField] private BoxesData _data;

        [Header("Лента наград")]
        [SerializeField] private RectTransform _content;
        [SerializeField] private RewardBar _barPrefab;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private ImageMover _imageMover;
        [SerializeField] private RectTransform[] _pages;

        [Header("Компоненты")]
        [SerializeField] private WheelStrip _strip;
        [SerializeField] private WheelSpinAnimation _spinAnimation;
        [SerializeField] private WheelRewardDisplay _rewardDisplay;
        [SerializeField] private WheelButton _spinButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private SmoothScroll _scroll;
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;

        [Header("Анимация спина")]
        [SerializeField] private int _minSpins = 5;
        [SerializeField] private int _maxSpins = 8;
        [SerializeField] private string _emptySymbol;

        [Header("Лента")]
        [SerializeField] private int _bufferSize = 5;

        private int _currentPageIndex = -1;
        private int _lastDisplayedRewardIndex = -1;

        private bool _initialized;

        private bool _isSpinning;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_homeButton)
                _homeButton.onClick.AddListener(OnHomeButtonClick);

            if (_spinButton)
                _spinButton.Clicked += OnSpinButtonClick;

            if (_initialized)
                return;

            Initialize();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_homeButton)
                _homeButton.onClick.RemoveListener(OnHomeButtonClick);

            if (_spinButton)
                _spinButton.Clicked -= OnSpinButtonClick;

            if (_spinAnimation)
                _spinAnimation.Kill();

            if (_rewardDisplay)
                _rewardDisplay.KillAnimation();

            _isSpinning = false;
        }

        public void OpenFirstPage()
        {
            _currentPageIndex = 0;
            ScrollToPage(0);
        }

        public void OpenSecondPage()
        {
            _currentPageIndex = 1;
            ScrollToPage(1);
        }

        private void OnHomeButtonClick()
        {
            if (_isSpinning) return;

            OpenFirstPage();
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            if (!_boxContent || !_boxBarPrefab || !_data ||
                !_content || !_barPrefab || !_viewport ||
                !_strip || !_spinAnimation || !_rewardDisplay || !_spinButton)
            {
                Debug.LogWarning("[Wheel] Не все ссылки назначены в Inspector.", this);
                return;
            }

            foreach (Transform child in _boxContent)
            {
                if (child)
                    Destroy(child.gameObject);
            }

            foreach (var box in _data.Boxes)
            {
                if (box == null)
                    continue;

                var bar = Instantiate(_boxBarPrefab, _boxContent);
                bar.Init(box, OpenBox);
            }

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_boxContent);
            Canvas.ForceUpdateCanvases();

            _rewardDisplay.Clear(_emptySymbol);
            _spinButton.SetAvailable(false);

            _initialized = true;
            SetFirstPageImmediately();
        }

        private void OnSpinButtonClick()
        {
            Spin();
        }

        private void OpenBox(BoxesData.Box box)
        {
            if (!_initialized || _isSpinning || box == null)
                return;

            if (box.boxCount <= 0 || box.keyCount <= 0)
                return;

            if (!_strip.Initialize(_content, _barPrefab, box, _viewport, _bufferSize))
            {
                Debug.LogWarning("[Wheel] Не удалось создать ленту наград для лутбокса.", this);
                return;
            }

            _rewardDisplay.KillAnimation();
            _rewardDisplay.Clear(_emptySymbol);
            _lastDisplayedRewardIndex = -1;

            _spinButton.SetAvailable(true);
            OpenSecondPage();
        }

        private void SetFirstPageImmediately()
        {
            _currentPageIndex = 0;
            SetPageImmediately(0);
        }

        private void SetPageImmediately(int pageIndex)
        {
            if (!_scroll || !_scroll.GetScrollRect || _pages == null ||
                pageIndex < 0 || pageIndex >= _pages.Length)
                return;

            var scrollRect = _scroll.GetScrollRect;
            var content = scrollRect.content;
            var viewport = scrollRect.viewport;

            if (!content || !viewport || !_pages[pageIndex])
                return;

            var target = _pages[pageIndex];

            var targetCenter = content.InverseTransformPoint(
                target.TransformPoint(target.rect.center));

            var viewportCenter = content.InverseTransformPoint(
                viewport.TransformPoint(viewport.rect.center));

            var targetX = content.anchoredPosition.x + viewportCenter.x - targetCenter.x;

            _scroll.Stop();
            _scroll.SetPositionX(targetX);
        }

        private void ScrollToPage(int pageIndex)
        {
            if (!_scroll || !_scroll.GetScrollRect || _pages == null ||
                pageIndex < 0 || pageIndex >= _pages.Length)
                return;

            var scrollRect = _scroll.GetScrollRect;
            var content = scrollRect.content;
            var viewport = scrollRect.viewport;

            if (!content || !viewport || !_pages[pageIndex])
                return;

            var target = _pages[pageIndex];

            var targetCenter = content.InverseTransformPoint(
                target.TransformPoint(target.rect.center));

            var viewportCenter = content.InverseTransformPoint(
                viewport.TransformPoint(viewport.rect.center));

            var targetX = content.anchoredPosition.x + viewportCenter.x - targetCenter.x;

            _scroll.ScrollToX(targetX);
        }

        private void Spin()
        {
            if (!_initialized || _isSpinning || !_strip)
                return;

            var count = _strip.Rewards.Count;

            if (count == 0 || _strip.ItemWidth <= 0f || _strip.CircleLength <= 0f)
                return;

            _isSpinning = true;

            _spinAnimation.Kill();
            _rewardDisplay.KillAnimation();
            _rewardDisplay.Clear(_emptySymbol);
            _spinButton.SetAvailable(false);

            if (_imageMover)
                _imageMover.Disable();

            _lastDisplayedRewardIndex = -1;

            var minSpins = Mathf.Max(1, _minSpins);
            var maxSpins = Mathf.Max(minSpins, _maxSpins);

            var fullSpins = Random.Range(minSpins, maxSpins + 1);
            var additionalSteps = Random.Range(1, count + 1);
            var totalSteps = fullSpins * count + additionalSteps;

            var startScroll = _strip.AbsoluteScroll;
            var targetScroll = startScroll + totalSteps * _strip.ItemWidth;

            _spinAnimation.Play(startScroll, targetScroll, OnSpinUpdate, FinishSpin);
        }

        private void OnSpinUpdate(float scroll)
        {
            _strip.SetScroll(scroll);

            var rewardIndex = _strip.GetRewardIndex();

            if (rewardIndex == _lastDisplayedRewardIndex)
                return;

            _lastDisplayedRewardIndex = rewardIndex;

            var reward = _strip.GetReward(rewardIndex);

            if (reward != null)
                _rewardDisplay.UpdatePreview(reward);
        }

        private void FinishSpin()
        {
            if (!_strip)
            {
                _isSpinning = false;
                return;
            }

            var itemWidth = _strip.ItemWidth;

            if (itemWidth <= 0f)
            {
                _isSpinning = false;
                _spinButton.SetAvailable(true);
                return;
            }

            var finalScroll = Mathf.Round(_strip.AbsoluteScroll / itemWidth) * itemWidth;

            _strip.SetScroll(finalScroll);

            var rewardIndex = _strip.GetRewardIndex(true);
            var reward = _strip.GetReward(rewardIndex);

            if (reward != null)
                _rewardDisplay.Show(reward);

            _strip.AnimateWinningBar(rewardIndex);

            _isSpinning = false;
            _spinButton.SetAvailable(true);

            if (_imageMover)
                _imageMover.Enable();
        }

        protected override void Show()
        {
            SetFirstPageImmediately();

            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition);
        }

        protected override void Hide()
        {
            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Close(
                _canvasGroup,
                _rectTransform,
                FinishTransition);
        }
    }
}
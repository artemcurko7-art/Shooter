using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Map
{
    public class MapSelector : MonoBehaviour
    {
        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private MapBar _prefab;
        [SerializeField] private RectTransform _content;
        [SerializeField] private MapData _data;

        [Header("Кнопки")]
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [Header("Скролл")]
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        [Header("Анимация выбранной карты")]
        [SerializeField] private float _selectedScale = 1.15f;
        [SerializeField] private float _scaleDuration = 0.3f;
        [SerializeField] private Ease _scaleEase = Ease.OutBack;

        private readonly List<MapBar> _bars = new();
        private readonly List<Vector3> _initialScales = new();

        private Tween _scrollTween;
        private Tween _scaleTween;

        private int _currentBarIndex;

        private void Awake()
        {
            InitializeBars();
            SubscribeButtons();
        }

        private void Start()
        {
            RebuildLayout();
            UpdateButtons();
            SetInitialPosition();
            SetInitialScale();
        }

        private void OnDestroy()
        {
            _scrollTween?.Kill();
            _scaleTween?.Kill();

            KillBarScaleTweens();
            UnsubscribeButtons();
        }

        private void InitializeBars()
        {
            if (!_prefab || !_content || !_data)
                return;

            _bars.Clear();
            _initialScales.Clear();

            foreach (var map in _data.Maps)
            {
                var bar = Instantiate(_prefab, _content);
                bar.Init(map);

                _bars.Add(bar);
                _initialScales.Add(bar.transform.localScale);
            }
        }

        private void SubscribeButtons()
        {
            if (_previousButton)
                _previousButton.onClick.AddListener(ScrollPrevious);

            if (_nextButton)
                _nextButton.onClick.AddListener(ScrollNext);
        }

        private void UnsubscribeButtons()
        {
            if (_previousButton)
                _previousButton.onClick.RemoveListener(ScrollPrevious);

            if (_nextButton)
                _nextButton.onClick.RemoveListener(ScrollNext);
        }

        private void ScrollPrevious()
        {
            if (_currentBarIndex <= 0)
                return;

            ScrollToIndex(_currentBarIndex - 1);
        }

        private void ScrollNext()
        {
            if (_currentBarIndex >= _bars.Count - 1)
                return;

            ScrollToIndex(_currentBarIndex + 1);
        }

        private void ScrollToIndex(int index)
        {
            if (!IsValidIndex(index))
                return;

            if (!_scrollRect || !_scrollRect.viewport || !_content)
                return;

            RebuildLayout();

            var targetNormalized = CalculateNormalizedPosition(index);

            AnimateBarSelection(index);

            _currentBarIndex = index;

            AnimateScroll(targetNormalized);
            UpdateButtons();
        }

        private float CalculateNormalizedPosition(int index)
        {
            var viewport = _scrollRect.viewport;
            var content = _scrollRect.content;
            var targetBar = _bars[index];

            if (!viewport || !content || !targetBar)
                return _scrollRect.horizontalNormalizedPosition;

            var targetRect = targetBar.transform as RectTransform;

            if (!targetRect)
                return _scrollRect.horizontalNormalizedPosition;

            var corners = new Vector3[4];
            targetRect.GetWorldCorners(corners);

            var worldCenter = (corners[0] + corners[2]) * 0.5f;

            var screenPoint = RectTransformUtility.WorldToScreenPoint(
                null,
                worldCenter
            );

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewport,
                    screenPoint,
                    null,
                    out var viewportPoint))
            {
                return _scrollRect.horizontalNormalizedPosition;
            }

            var viewportCenterX = viewport.rect.center.x;
            var deltaX = viewportPoint.x - viewportCenterX;

            var contentWidth = content.rect.width;
            var viewportWidth = viewport.rect.width;
            var scrollRange = contentWidth - viewportWidth;

            if (scrollRange <= 0f)
                return 0f;

            var currentNormalized = _scrollRect.horizontalNormalizedPosition;
            var normalizedOffset = deltaX / scrollRange;
            var targetNormalized = currentNormalized + normalizedOffset;

            return Mathf.Clamp01(targetNormalized);
        }

        private void AnimateScroll(float targetNormalized)
        {
            _scrollTween?.Kill();

            var startNormalized = _scrollRect.horizontalNormalizedPosition;

            if (Mathf.Abs(startNormalized - targetNormalized) < 0.001f)
            {
                _scrollRect.horizontalNormalizedPosition = targetNormalized;
                return;
            }

            _scrollTween = DOTween.To(
                    () => _scrollRect.horizontalNormalizedPosition,
                    value => _scrollRect.horizontalNormalizedPosition = value,
                    targetNormalized,
                    _duration
                )
                .SetEase(_ease)
                .OnComplete(() =>
                {
                    _scrollRect.horizontalNormalizedPosition = targetNormalized;
                    _scrollTween = null;
                });
        }

        private void AnimateBarSelection(int selectedIndex)
        {
            for (var i = 0; i < _bars.Count; i++)
            {
                if (!_bars[i])
                    continue;

                var isSelected = i == selectedIndex;
                var targetScale = _initialScales[i];

                if (isSelected)
                    targetScale *= _selectedScale;

                _bars[i].transform.DOKill();

                _bars[i].transform
                    .DOScale(targetScale, _scaleDuration)
                    .SetEase(isSelected ? _scaleEase : Ease.OutQuad);

                _bars[i].SetSelected(isSelected);
            }
        }

        private void SetInitialPosition()
        {
            if (_bars.Count == 0 || !_scrollRect)
                return;

            _currentBarIndex = Mathf.Clamp(
                _currentBarIndex,
                0,
                _bars.Count - 1
            );

            var targetNormalized = CalculateNormalizedPosition(
                _currentBarIndex
            );

            _scrollRect.horizontalNormalizedPosition = targetNormalized;
        }

        private void SetInitialScale()
        {
            for (var i = 0; i < _bars.Count; i++)
            {
                if (!_bars[i])
                    continue;

                var isSelected = i == _currentBarIndex;
                var targetScale = _initialScales[i];

                if (isSelected)
                    targetScale *= _selectedScale;

                _bars[i].transform.localScale = targetScale;
                _bars[i].SetSelected(isSelected, false);
            }
        }

        private void KillBarScaleTweens()
        {
            foreach (var bar in _bars.Where(bar => bar))
            {
                bar.transform.DOKill();
            }
        }

        private void RebuildLayout()
        {
            if (!_content)
                return;

            Canvas.ForceUpdateCanvases();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);

            Canvas.ForceUpdateCanvases();
        }

        private void UpdateButtons()
        {
            if (_previousButton)
                _previousButton.interactable = _currentBarIndex > 0;

            if (_nextButton)
            {
                _nextButton.interactable =
                    _currentBarIndex < _bars.Count - 1;
            }
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < _bars.Count;
        }
    }
}
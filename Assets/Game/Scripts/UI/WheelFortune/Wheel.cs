using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.WheelFortune
{
    public class Wheel : Window
    {
        private readonly List<RewardBar> _bars = new();
        private readonly List<RewardData.Reward> _strip = new();
        private readonly List<RewardData.Reward> _shuffledRewards = new();

        [Header("Ссылки")]
        [SerializeField] private RectTransform _content;
        [SerializeField] private RewardBar _barPrefab;
        [SerializeField] private RewardData _data;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private Button _spinButton;
        [SerializeField] private TMP_Text _rewardPreviewText;
        [SerializeField] private Image _rewardPreviewImage;
        [SerializeField] private AppearAnimation _rewardAnimation;

        [Header("Анимация спина")]
        [SerializeField] private float _spinDuration = 4f;
        [SerializeField] private int _minSpins = 5;
        [SerializeField] private int _maxSpins = 8;
        [SerializeField] private Sprite _greenButton;
        [SerializeField] private Sprite _redButton;
        [SerializeField] private TMP_Text _spinButtonText;
        [SerializeField] private Ease _spinEase = Ease.InQuart;
        [SerializeField] private string _emptySymbol;

        [Header("Анимация стрелки")]
        [SerializeField] private ImageMover _imageMover;

        [Header("Анимация награды")]
        [SerializeField] private float _rewardAnimDuration = 0.1f;
        [SerializeField] private float _rewardScale = 1.15f;

        [Header("Лента")]
        [SerializeField] private int _bufferSize = 5;

        private float _itemWidth;
        private float _circleLength;
        private float _centerContentX;
        private float _absoluteScroll;

        private int _currentRewardIndex;
        private int _lastDisplayedRewardIndex = -1;

        private bool _isSpinning;
        private bool _initialized;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_spinButton)
                _spinButton.onClick.AddListener(Spin);

            if (!_initialized)
                Initialize();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_spinButton)
                _spinButton.onClick.RemoveListener(Spin);

            if (_content)
                _content.DOKill();

            if (_rewardAnimation)
                _rewardAnimation.KillAnimation();

            _isSpinning = false;
        }

        private static Vector3 GetWorldCenter(RectTransform rect)
        {
            var corners = new Vector3[4];

            rect.GetWorldCorners(corners);

            return (corners[0] + corners[2]) * 0.5f;
        }

        private static int Mod(int value, int modulo)
        {
            if (modulo <= 0)
                return 0;

            var result = value % modulo;

            if (result < 0)
                result += modulo;

            return result;
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            ShuffleRewards();
            BuildStrip();
            SpawnBars();

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
            Canvas.ForceUpdateCanvases();

            if (!UpdateItemWidth())
                return;

            CalculateCircleLength();
            SetInitialPosition();

            if (_rewardPreviewText)
                _rewardPreviewText.text = _emptySymbol;

            if (_rewardAnimation)
                _rewardAnimation.SetVisible();

            _initialized = true;
            SwitchVisible(true);
        }

        private void SwitchVisible(bool isActive)
        {
            if (isActive)
            {
                _spinButton.enabled = true;
                _spinButton.image.sprite = _greenButton;
                _spinButtonText.text = Localization.GetSpinButtonText();
            }
            else
            {
                _spinButton.enabled = false;
                _spinButton.image.sprite = _redButton;
                _spinButtonText.text = Localization.GetNotAvailableButtonText();
            }
        }

        private void ShuffleRewards()
        {
            _shuffledRewards.Clear();

            foreach (var reward in _data.Rewards.Where(reward => reward != null))
                _shuffledRewards.Add(reward);

            for (var i = _shuffledRewards.Count - 1; i > 0; i--)
            {
                var randomIndex = Random.Range(0, i + 1);

                (
                    _shuffledRewards[i],
                    _shuffledRewards[randomIndex]
                ) = (
                    _shuffledRewards[randomIndex],
                    _shuffledRewards[i]
                );
            }
        }

        private void BuildStrip()
        {
            _strip.Clear();

            var count = _shuffledRewards.Count;

            if (count == 0)
                return;

            var buffer = Mathf.Min(_bufferSize, count);

            for (var i = count - buffer; i < count; i++)
                _strip.Add(_shuffledRewards[Mod(i, count)]);

            for (var i = 0; i < count; i++)
                _strip.Add(_shuffledRewards[i]);

            for (var i = 0; i < buffer; i++)
                _strip.Add(_shuffledRewards[i % count]);
        }

        private void SpawnBars()
        {
            foreach (Transform child in _content)
            {
                if (child)
                    Destroy(child.gameObject);
            }

            _bars.Clear();

            foreach (var reward in _strip)
            {
                if (reward == null)
                    continue;

                var bar = Instantiate(_barPrefab, _content);
                bar.Init(reward);
                _bars.Add(bar);
            }
        }

        private bool UpdateItemWidth()
        {
            if (_bars.Count == 0)
                return false;

            var rect = _bars[0].transform as RectTransform;

            if (!rect)
                return false;

            _itemWidth = rect.rect.width;

            return _itemWidth > 0f;
        }

        private void CalculateCircleLength()
        {
            _circleLength = _shuffledRewards.Count * _itemWidth;
        }

        private void SetInitialPosition()
        {
            if (_bars.Count == 0)
                return;

            var buffer = Mathf.Min(_bufferSize, _shuffledRewards.Count);
            var centerIndex = Mathf.Min(buffer, _bars.Count - 1);

            var bar = _bars[centerIndex].transform as RectTransform;

            if (!bar)
                return;

            var viewportCenter = GetWorldCenter(_viewport);
            var barCenter = GetWorldCenter(bar);
            var difference = viewportCenter.x - barCenter.x;

            var contentPosition = _content.position;
            contentPosition.x += difference;
            _content.position = contentPosition;

            _centerContentX = _content.anchoredPosition.x;
            _absoluteScroll = 0f;
            _currentRewardIndex = 0;

            ApplyScroll();
        }

        private void ApplyScroll()
        {
            if (_circleLength <= 0f)
                return;

            var visualScroll = Mathf.Repeat(_absoluteScroll, _circleLength);

            _content.anchoredPosition = new Vector2(
                _centerContentX - visualScroll,
                _content.anchoredPosition.y
            );
        }

        private void Spin()
        {
            if (!_initialized || _isSpinning)
                return;

            var count = _shuffledRewards.Count;

            if (count == 0 || _itemWidth <= 0f || _circleLength <= 0f)
                return;

            _isSpinning = true;
            _content.DOKill();

            if (_rewardAnimation)
                _rewardAnimation.KillAnimation();

            if (_rewardPreviewText)
                _rewardPreviewText.text = _emptySymbol;

            SwitchVisible(false);

            if (_imageMover)
                _imageMover.Disable();

            _lastDisplayedRewardIndex = -1;

            var targetIndex = Random.Range(0, count);
            var stepsToTarget = Mod(targetIndex - _currentRewardIndex, count);

            if (stepsToTarget == 0)
                stepsToTarget = count;

            var fullSpins = Random.Range(_minSpins, _maxSpins + 1);
            var totalSteps = fullSpins * count + stepsToTarget;

            var startScroll = _absoluteScroll;
            var targetScroll = startScroll + totalSteps * _itemWidth;

            DOTween.To(
                    () => startScroll,
                    value =>
                    {
                        _absoluteScroll = value;
                        ApplyScroll();
                        UpdateRewardDuringSpin();
                    },
                    targetScroll,
                    _spinDuration
                )
                .SetEase(_spinEase)
                .OnComplete(() =>
                {
                    _currentRewardIndex = targetIndex;

                    UpdateRewardText(true);

                    if (_currentRewardIndex >= 0 && _currentRewardIndex < _bars.Count)
                    {
                        _bars[_currentRewardIndex]
                            .AnimateIcon(_rewardScale, _rewardAnimDuration);
                    }

                    _isSpinning = false;
                    SwitchVisible(true);

                    if (_imageMover)
                        _imageMover.Enable();
                });
        }

        private void UpdateRewardDuringSpin()
        {
            if (_shuffledRewards.Count == 0)
                return;

            var passedSteps = Mathf.FloorToInt(_absoluteScroll / _itemWidth);
            var rewardIndex = Mod(passedSteps, _shuffledRewards.Count);

            if (rewardIndex == _lastDisplayedRewardIndex)
                return;

            _lastDisplayedRewardIndex = rewardIndex;

            var reward = _shuffledRewards[rewardIndex];

            if (reward == null)
                return;

            UpdateRewardPreview(reward);
        }

        private void UpdateRewardText(bool animate)
        {
            if (_shuffledRewards.Count == 0)
                return;

            var reward = _shuffledRewards[_currentRewardIndex];

            if (reward == null)
                return;

            if (!animate)
            {
                UpdateRewardPreview(reward);
                return;
            }

            if (!_rewardAnimation)
            {
                UpdateRewardPreview(reward);
                return;
            }

            _rewardAnimation.Play(() => { UpdateRewardPreview(reward); });
        }

        private void UpdateRewardPreview(RewardData.Reward reward)
        {
            if (reward == null)
                return;

            if (_rewardPreviewText)
                _rewardPreviewText.text = reward.GetLocalizedName(YG2.lang);

            if (_rewardPreviewImage)
                _rewardPreviewImage.sprite = reward.icon;
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration
            );
        }

        protected override void Hide()
        {
            if (IsTransitionActive)
                return;

            if (_transition)
            {
                _transition.Close(
                    _canvasGroup,
                    _rectTransform
                );
            }
        }
    }
}
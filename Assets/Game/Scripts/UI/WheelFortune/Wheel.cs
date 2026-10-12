
using DG.Tweening;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class Wheel : Window
    {
        [Header("Ссылки")]
        [SerializeField] private RectTransform _content;
        [SerializeField] private RewardBar _barPrefab;
        [SerializeField] private RewardData _data;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private Button _spinButton;
        [SerializeField] private ImageMover _imageMover;

        [Header("Компоненты")]
        [SerializeField] private WheelStrip _strip;
        [SerializeField] private WheelSpinAnimation _spinAnimation;
        [SerializeField] private WheelRewardDisplay _rewardDisplay;
        [SerializeField] private WheelButton _wheelButton;

        [Header("Анимация спина")]
        [SerializeField] private float _spinDuration = 4f;
        [SerializeField] private int _minSpins = 5;
        [SerializeField] private int _maxSpins = 8;
        [SerializeField] private Ease _spinEase = Ease.InQuart;
        [SerializeField] private string _emptySymbol;

        [Header("Анимация награды")]
        [SerializeField] private float _rewardAnimDuration = 0.1f;
        [SerializeField] private float _rewardScale = 1.15f;

        [Header("Лента")]
        [SerializeField] private int _bufferSize = 5;

        private bool _isSpinning;
        private bool _initialized;
        private int _lastDisplayedRewardIndex = -1;

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

            if (_spinAnimation)
                _spinAnimation.Kill();

            if (_rewardDisplay)
                _rewardDisplay.KillAnimation();

            _isSpinning = false;
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            if (!_strip || !_spinAnimation || !_rewardDisplay || !_wheelButton)
                return;

            if (!_strip.Initialize(
                    _content,
                    _barPrefab,
                    _data,
                    _viewport,
                    _bufferSize))
            {
                return;
            }

            _rewardDisplay.Clear(_emptySymbol);
            _wheelButton.SetAvailable(true);

            _initialized = true;

            SwitchVisible(true);
        }

        private void SwitchVisible(bool isActive)
        {
            if (_wheelButton)
                _wheelButton.SetAvailable(isActive);
        }

        private void Spin()
        {
            if (!_initialized || _isSpinning)
                return;

            var count = _strip.Rewards.Count;

            if (count == 0 || _strip.ItemWidth <= 0f || _strip.CircleLength <= 0f)
                return;

            _isSpinning = true;

            _spinAnimation.Kill();
            _rewardDisplay.KillAnimation();
            _rewardDisplay.Clear(_emptySymbol);
            _wheelButton.SetAvailable(false);

            if (_imageMover)
                _imageMover.Disable();

            _lastDisplayedRewardIndex = -1;

            var fullSpins = Random.Range(_minSpins, _maxSpins + 1);
            var additionalSteps = Random.Range(1, count + 1);
            var totalSteps = fullSpins * count + additionalSteps;

            var startScroll = _strip.AbsoluteScroll;
            var targetScroll = startScroll + totalSteps * _strip.ItemWidth;

            _spinAnimation.Play(
                startScroll,
                targetScroll,
                _spinDuration,
                _spinEase,
                OnSpinUpdate,
                FinishSpin
            );
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
            var itemWidth = _strip.ItemWidth;

            if (itemWidth <= 0f)
            {
                _isSpinning = false;
                SwitchVisible(true);
                return;
            }

            var finalScroll = Mathf.Round(_strip.AbsoluteScroll / itemWidth) * itemWidth;

            _strip.SetScroll(finalScroll);

            var rewardIndex = _strip.GetRewardIndex(true);
            var reward = _strip.GetReward(rewardIndex);

            if (reward != null)
                _rewardDisplay.Show(reward);

            _strip.AnimateWinningBar(
                rewardIndex,
                _rewardScale,
                _rewardAnimDuration
            );

            _isSpinning = false;
            SwitchVisible(true);

            if (_imageMover)
                _imageMover.Enable();
        }

        protected override void Show()
        {
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
                FinishTransition
            );
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
                FinishTransition
            );
        }
    }
}

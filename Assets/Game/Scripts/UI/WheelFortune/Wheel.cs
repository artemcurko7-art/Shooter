using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private TMP_Text _reward;

        [Header("Анимация")]
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

            if (_spinButton != null)
                _spinButton.onClick.AddListener(Spin);

            if (!_initialized)
                Initialize();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_spinButton != null)
                _spinButton.onClick.RemoveListener(Spin);

            if (_content != null)
                _content.DOKill();

            if (_reward != null)
            {
                _reward.DOKill();
                _reward.transform.DOKill();
            }

            _isSpinning = false;
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            if (_content == null ||
                _barPrefab == null ||
                _data == null ||
                _viewport == null ||
                _reward == null)
            {
                Debug.LogError(
                    $"{nameof(Wheel)}: Не все ссылки назначены.",
                    this
                );

                return;
            }

            if (_data.Rewards == null ||
                _data.Rewards.Count == 0)
            {
                Debug.LogError(
                    $"{nameof(Wheel)}: В RewardData отсутствуют награды.",
                    this
                );

                return;
            }

            ShuffleRewards();
            BuildStrip();
            SpawnBars();

            Canvas.ForceUpdateCanvases();

            LayoutRebuilder.ForceRebuildLayoutImmediate(
                _content
            );

            Canvas.ForceUpdateCanvases();

            if (!UpdateItemWidth())
                return;

            CalculateCircleLength();
            SetInitialPosition();

            _initialized = true;
        }

        private void ShuffleRewards()
        {
            _shuffledRewards.Clear();

            foreach (var reward in _data.Rewards)
            {
                if (reward != null)
                    _shuffledRewards.Add(reward);
            }

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

            var buffer = Mathf.Min(
                _bufferSize,
                count
            );

            for (var i = count - buffer; i < count; i++)
            {
                _strip.Add(
                    _shuffledRewards[
                        Mod(i, count)
                    ]
                );
            }

            for (var i = 0; i < count; i++)
            {
                _strip.Add(
                    _shuffledRewards[i]
                );
            }

            for (var i = 0; i < buffer; i++)
            {
                _strip.Add(
                    _shuffledRewards[
                        i % count
                    ]
                );
            }
        }

        private void SpawnBars()
        {
            foreach (Transform child in _content)
            {
                if (child != null)
                    Destroy(child.gameObject);
            }

            _bars.Clear();

            foreach (var reward in _strip)
            {
                if (reward == null)
                    continue;

                var bar = Instantiate(
                    _barPrefab,
                    _content
                );

                bar.Init(reward);

                _bars.Add(bar);
            }
        }

        private bool UpdateItemWidth()
        {
            if (_bars.Count == 0)
                return false;

            var rect =
                _bars[0].transform as RectTransform;

            if (rect == null)
            {
                Debug.LogError(
                    $"{nameof(Wheel)}: RewardBar не имеет RectTransform.",
                    _bars[0]
                );

                return false;
            }

            _itemWidth = rect.rect.width;

            if (_itemWidth <= 0f)
            {
                Debug.LogError(
                    $"{nameof(Wheel)}: ширина RewardBar равна 0.",
                    this
                );

                return false;
            }

            return true;
        }

        private void CalculateCircleLength()
        {
            _circleLength =
                _shuffledRewards.Count *
                _itemWidth;
        }

        private Vector3 GetWorldCenter(
            RectTransform rect)
        {
            var corners = new Vector3[4];

            rect.GetWorldCorners(corners);

            return (
                corners[0] +
                corners[2]
            ) * 0.5f;
        }

        private void SetInitialPosition()
        {
            if (_bars.Count == 0)
                return;

            var centerIndex =
                Mathf.Min(
                    _bufferSize,
                    _bars.Count - 1
                );

            var bar =
                _bars[centerIndex]
                    .transform as RectTransform;

            if (bar == null)
                return;

            var viewportCenter =
                GetWorldCenter(_viewport);

            var barCenter =
                GetWorldCenter(bar);

            var difference =
                viewportCenter.x -
                barCenter.x;

            var contentPosition =
                _content.position;

            contentPosition.x += difference;

            _content.position =
                contentPosition;

            _centerContentX =
                _content.anchoredPosition.x;

            _absoluteScroll = 0f;
            _currentRewardIndex = 0;

            ApplyScroll();

            UpdateRewardText(true);
        }

        private void ApplyScroll()
        {
            if (_circleLength <= 0f)
                return;

            var visualScroll =
                Mathf.Repeat(
                    _absoluteScroll,
                    _circleLength
                );

            _content.anchoredPosition =
                new Vector2(
                    _centerContentX -
                    visualScroll,
                    _content.anchoredPosition.y
                );
        }

        private void Spin()
        {
            if (!_initialized ||
                _isSpinning)
                return;

            var count =
                _shuffledRewards.Count;

            if (count == 0 ||
                _itemWidth <= 0f ||
                _circleLength <= 0f)
                return;

            _isSpinning = true;

            _content.DOKill();

            if (_reward != null)
            {
                _reward.DOKill();
                _reward.transform.DOKill();

                _reward.text =
                    _emptySymbol;
            }

            _lastDisplayedRewardIndex = -1;

            var targetIndex =
                Random.Range(
                    0,
                    count
                );

            var stepsToTarget =
                Mod(
                    targetIndex -
                    _currentRewardIndex,
                    count
                );

            if (stepsToTarget == 0)
                stepsToTarget = count;

            var fullSpins =
                Random.Range(
                    _minSpins,
                    _maxSpins + 1
                );

            var totalSteps =
                fullSpins * count +
                stepsToTarget;

            var startScroll =
                _absoluteScroll;

            var targetScroll =
                startScroll +
                totalSteps * _itemWidth;

            DOTween.To(
                    () => startScroll,
                    value =>
                    {
                        _absoluteScroll =
                            value;

                        ApplyScroll();

                        UpdateRewardDuringSpin();
                    },
                    targetScroll,
                    _spinDuration
                )
                .SetEase(_spinEase)
                .OnComplete(() =>
                {
                    _absoluteScroll =
                        targetScroll;

                    ApplyScroll();

                    _currentRewardIndex =
                        targetIndex;

                    UpdateRewardText(true);

                    _isSpinning = false;
                });
        }

        private void UpdateRewardDuringSpin()
        {
            var count =
                _shuffledRewards.Count;

            if (count == 0)
                return;

            var passedSteps =
                Mathf.FloorToInt(
                    _absoluteScroll /
                    _itemWidth
                );

            var rewardIndex =
                Mod(
                    passedSteps,
                    count
                );

            if (rewardIndex ==
                _lastDisplayedRewardIndex)
                return;

            _lastDisplayedRewardIndex =
                rewardIndex;

            var reward =
                _shuffledRewards[
                    rewardIndex
                ];

            if (reward == null ||
                _reward == null)
                return;

            var rewardName =
                reward.GetLocalizedName(
                    YG2.lang
                );

            _reward.DOKill();
            _reward.transform.DOKill();

            _reward.text =
                rewardName;

            _reward.transform.localScale =
                Vector3.one *
                _rewardScale;

            _reward.transform
                .DOScale(
                    Vector3.one,
                    _rewardAnimDuration
                )
                .SetEase(
                    Ease.OutBack
                );
        }

        private void UpdateRewardText(
            bool animate)
        {
            if (_shuffledRewards.Count == 0 ||
                _reward == null)
                return;

            var reward =
                _shuffledRewards[
                    _currentRewardIndex
                ];

            if (reward == null)
                return;

            var rewardName =
                reward.GetLocalizedName(
                    YG2.lang
                );

            _lastDisplayedRewardIndex =
                _currentRewardIndex;

            if (!animate)
            {
                _reward.text =
                    rewardName;

                return;
            }

            _reward.DOKill();
            _reward.transform.DOKill();

            _reward.transform.localScale =
                Vector3.one;

            var sequence =
                DOTween.Sequence();

            sequence.Append(
                _reward.DOFade(
                    0f,
                    _rewardAnimDuration
                )
            );

            sequence.AppendCallback(() =>
            {
                _reward.text =
                    rewardName;
            });

            sequence.Append(
                _reward.DOFade(
                    1f,
                    _rewardAnimDuration
                )
            );

            sequence.Join(
                _reward.transform.DOScale(
                    _rewardScale,
                    _rewardAnimDuration
                )
                .SetEase(
                    Ease.OutBack
                )
            );

            sequence.Append(
                _reward.transform.DOScale(
                    Vector3.one,
                    _rewardAnimDuration
                )
            );
        }

        private static int Mod(
            int value,
            int modulo)
        {
            if (modulo <= 0)
                return 0;

            var result =
                value % modulo;

            if (result < 0)
                result += modulo;

            return result;
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

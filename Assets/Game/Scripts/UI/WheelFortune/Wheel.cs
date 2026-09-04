using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

        [Header("Анимация")]
        [SerializeField] private float _spinDuration = 4f;
        [SerializeField] private int _minSpins = 5;
        [SerializeField] private int _maxSpins = 8;
        [SerializeField] private Ease _spinEase = Ease.InQuart;

        private readonly List<RewardBar> _bars = new();
        private readonly List<RewardData.Reward> _strip = new();
        private int _bufferSize = 5;
        private bool _isSpinning;
        private float _itemWidth;

        protected override void OnEnable()
        {
            base.OnEnable();
            _spinButton.onClick.AddListener(Spin);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _spinButton.onClick.RemoveListener(Spin);
        }

        private void Start()
        {
            InitializeRewards();
        }

        private void InitializeRewards()
        {
            BuildStrip();
            SpawnBars();
            UpdateItemWidth();
            ResetContentPosition();
        }

        private void BuildStrip()
        {
            _strip.Clear();
            var baseRewards = _data.Rewards;
            var baseCount = baseRewards.Count;

            for (var i = baseCount - _bufferSize; i < baseCount; i++)
                _strip.Add(baseRewards[i]);

            for (var i = 0; i < baseCount; i++)
                _strip.Add(baseRewards[i]);

            for (var i = 0; i < _bufferSize; i++)
                _strip.Add(baseRewards[i % baseCount]);
        }

        private void SpawnBars()
        {
            foreach (Transform child in _content)
                Destroy(child.gameObject);

            _bars.Clear();

            for (var i = 0; i < _strip.Count; i++)
            {
                var bar = Instantiate(_barPrefab, _content);
                bar.Init(_strip[i]);
                _bars.Add(bar);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        }

        private void UpdateItemWidth()
        {
            if (_bars.Count == 0) return;
            _itemWidth = _bars[0].Rect.sizeDelta.x;
        }

        private void ResetContentPosition()
        {
            var viewportCenter = _viewport.rect.width * 0.5f;
            var firstMainBarX = _bars[_bufferSize].Rect.anchoredPosition.x;
            var startX = viewportCenter - firstMainBarX - _itemWidth * 0.5f;
            _content.anchoredPosition = new Vector2(startX, 0);
        }

        private void Spin()
        {
            if (_isSpinning) return;
            _isSpinning = true;

            var baseCount = _data.Rewards.Count;
            var targetBaseIndex = UnityEngine.Random.Range(0, baseCount);
            var spins = UnityEngine.Random.Range(_minSpins, _maxSpins + 1);

            var targetStripIndex = _bufferSize + targetBaseIndex;
            var targetBar = _bars[targetStripIndex];

            float distancePerSpin = baseCount * _itemWidth;
            float spinDistance = spins * distancePerSpin;

            float startX = _content.anchoredPosition.x;

            float viewportCenter = _viewport.rect.width * 0.5f;
    
            float targetBarLeftEdgePos = viewportCenter - (_itemWidth * 0.5f);

            float offsetToTarget = targetBar.Rect.anchoredPosition.x - targetBarLeftEdgePos;

            float targetX = startX - spinDistance - offsetToTarget; 
            
            _content.DOKill();

            _content.DOAnchorPosX(targetX, _spinDuration)
                .SetEase(_spinEase)
                .OnUpdate(OnSpinUpdate)
                .OnComplete(() =>
                {
                    _isSpinning = false;
                    var reward = _data.Rewards[targetBaseIndex];
                    Debug.Log($"Выпало: {reward.GetLocalizedName(YG2.lang)}");
                });
        }


        private void OnSpinUpdate()
        {
            float contentWidth = _content.rect.width;
            float viewWidth = _viewport.rect.width;
            float minX = -(contentWidth - viewWidth);

            float currentX = _content.anchoredPosition.x;

            if (currentX < minX)
            {
                float circleLength = _data.Rewards.Count * _itemWidth;
                _content.anchoredPosition = new Vector2(currentX + circleLength, 0);
            }
        }

        protected override void Show()
        {
            _transition.Open(_canvasGroup, _rectTransform, _openButton.transform.position, _scaleEase, _positionEase,
                _duration);
        }

        protected override void Hide()
        {
            if (IsTransitionActive) return;

            if (_transition)
            {
                _transition.Close(_canvasGroup, _rectTransform);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace Game.Scripts.UI.DailyReward
{
    public class DailyGiftSystem : Window
    {
        private const int DAYS_IN_WEEK = 7;

        private readonly List<Sprite> _usedFrames = new();
        private readonly List<DailyGiftBar> _bars = new();

        [Header("Зависимости")]
        [SerializeField] private DailyGiftBar _giftBarPrefab;
        [SerializeField] private DailyGiftBar _superGiftBarPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private DailyGiftData _data;
        [SerializeField] private List<Sprite> _frames;

        private void Start()
        {
            InitializeDailyRewards();
        }

        private void InitializeDailyRewards()
        {
            _usedFrames.Clear();
            _bars.Clear();

            for (var i = 0; i < DAYS_IN_WEEK - 1; i++)
            {
                var isAvailable = YG2.saves.IdAvailableDailyRewardCount >= i;
                var isTaken = YG2.saves.IdTakenDailyRewardCount >= i;

                var bar = Instantiate(_giftBarPrefab, _content);

                bar.Init(_data.Gifts[i], GetRandomFrame(), i + 1, isAvailable, isTaken);

                _bars.Add(bar);
            }

            var lastDayIndex = DAYS_IN_WEEK - 1;

            var isLastAvailable = YG2.saves.IdAvailableDailyRewardCount >= lastDayIndex;
            var isLastTaken = YG2.saves.IdTakenDailyRewardCount >= lastDayIndex;

            var superBar = Instantiate(_superGiftBarPrefab, _content);

            superBar.Init(_data.Gifts[lastDayIndex], DAYS_IN_WEEK, isLastAvailable, isLastTaken);

            _bars.Add(superBar);
        }

        private Sprite GetRandomFrame()
        {
            if (_frames == null || _frames.Count == 0)
                return null;

            if (_usedFrames.Count >= _frames.Count)
                _usedFrames.Clear();

            var availableFrames = _frames
                .Where(frame => frame != null && !_usedFrames.Contains(frame))
                .ToList();

            if (availableFrames.Count == 0)
            {
                _usedFrames.Clear();
                availableFrames = _frames
                    .Where(frame => frame != null)
                    .ToList();
            }

            if (availableFrames.Count == 0)
                return null;

            var randomIndex = Random.Range(0, availableFrames.Count);
            var selectedFrame = availableFrames[randomIndex];

            _usedFrames.Add(selectedFrame);

            return selectedFrame;
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
                _transition.Close(_canvasGroup, _rectTransform);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace Game.Scripts.UI.DailyGift
{
    public class DailyGiftSystem : Window
    {
        private const int DAYS_IN_WEEK = 7;

        private readonly List<Sprite> _usedFrames = new();
        private readonly List<DailyGiftBar> _bars = new();

        [Header("Зависимости")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private DailyGiftBar _giftBarPrefab;
        [SerializeField] private DailyGiftBar _superGiftBarPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private DailyGiftData _data;
        [SerializeField] private SliderGifts _sliderGifts;
        [SerializeField] private List<Sprite> _frames;
        [SerializeField] private BuyEffect _buyEffect;
        [SerializeField] private ContentDisplayer _contentDisplayer;

        private int _dayOfWeekNumber;

        public DailyGiftData.DailyGift CurrentGift { get; private set; }

        private void Start()
        {
            CheckNewWeek();

            InitializeTitleText();
            InitializeTodayReward();
            InitializeDailyRewards();
            Refresh();
        }

        public void Collect()
        {
            if (YG2.saves.TakenDailyGiftDays.Contains(_dayOfWeekNumber))
                return;

            YG2.saves.TakenDailyGiftDays.Add(_dayOfWeekNumber);
            YG2.saves.TotalCollectedGifts++;
            YG2.SaveProgress();

            _buyEffect.Animate(
                CurrentGift.icon,
                CurrentGift.count,
                _bars[_dayOfWeekNumber - 1].transform.position
            );

            Refresh();
            _sliderGifts.UpdateVisual();
        }

        private void CheckNewWeek()
        {
            var currentWeek = GetCurrentWeek();

            if (YG2.saves.DailyGiftWeek == currentWeek)
                return;

            YG2.saves.DailyGiftWeek = currentWeek;
            YG2.saves.TakenDailyGiftDays.Clear();

            YG2.SaveProgress();
        }

        private static int GetCurrentWeek()
        {
            return ISOWeek.GetWeekOfYear(DateTime.Today);
        }

        private void InitializeTodayReward()
        {
            _dayOfWeekNumber = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;
            CurrentGift = _data.Gifts[_dayOfWeekNumber - 1];
        }

        private void InitializeTitleText()
        {
            _title.text = Localization.GetDailyGiftsTitleText();
        }

        private void InitializeDailyRewards()
        {
            _usedFrames.Clear();
            _bars.Clear();

            for (var i = 0; i < DAYS_IN_WEEK - 1; i++)
            {
                var dayIndex = i + 1;
                var bar = Instantiate(_giftBarPrefab, _content);

                bar.Init(
                    _data.Gifts[i],
                    GetRandomFrame(),
                    dayIndex
                );

                _bars.Add(bar);
            }

            var superBar = Instantiate(
                _superGiftBarPrefab,
                _content
            );

            superBar.Init(
                _data.Gifts[DAYS_IN_WEEK - 1],
                DAYS_IN_WEEK
            );

            _bars.Add(superBar);

            _contentDisplayer.InitTargets(
                _bars.Select(bar => bar.transform).ToList()
            );
        }

        private Sprite GetRandomFrame()
        {
            if (_frames == null || _frames.Count == 0)
                return null;

            if (_usedFrames.Count >= _frames.Count)
                _usedFrames.Clear();

            var availableFrames = _frames
                .Where(frame => frame && !_usedFrames.Contains(frame))
                .ToList();

            if (availableFrames.Count == 0)
            {
                _usedFrames.Clear();

                availableFrames = _frames
                    .Where(frame => frame)
                    .ToList();
            }

            if (availableFrames.Count == 0)
                return null;

            var randomIndex = Random.Range(
                0,
                availableFrames.Count
            );

            var selectedFrame = availableFrames[randomIndex];

            _usedFrames.Add(selectedFrame);

            return selectedFrame;
        }

        private void Refresh()
        {
            foreach (var bar in _bars)
                bar.UpdateVisual();
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

            _contentDisplayer.Play();
            _sliderGifts.Play();
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
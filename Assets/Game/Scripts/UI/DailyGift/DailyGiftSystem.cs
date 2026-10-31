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
        [SerializeField] private PopEffect _popEffect;
        [SerializeField] private ContentDisplayer _contentDisplayer;

        private DailyGiftData.DailyGift _currentGift;
        private int _dayOfWeekNumber;

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
            if (_currentGift?.Reward == null)
                return;

            if (YG2.saves.TakenDailyGiftDays.Contains(_dayOfWeekNumber))
                return;

            YG2.saves.TakenDailyGiftDays.Add(_dayOfWeekNumber);
            YG2.saves.TotalCollectedGifts++;
            YG2.SaveProgress();

            var reward = _currentGift.Reward;
            var count = reward.Count;

            if (_popEffect)
                _popEffect.Animate(reward.Icon, count, true, _bars[_dayOfWeekNumber - 1].transform.position);

            Refresh();

            if (_sliderGifts)
                _sliderGifts.UpdateVisual();
        }

        private static void CheckNewWeek()
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
            var today = DateTime.Today;
            return today.Year * 100 + ISOWeek.GetWeekOfYear(today);
        }

        private void InitializeTodayReward()
        {
            if (!_data || _data.Gifts == null)
                return;

            _dayOfWeekNumber = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;

            if (_data.Gifts.Count < _dayOfWeekNumber)
                return;

            _currentGift = _data.Gifts[_dayOfWeekNumber - 1];
        }

        private void InitializeTitleText()
        {
            if (_title)
                _title.text = Localization.GetDailyGiftsTitleText();
        }

        private void InitializeDailyRewards()
        {
            if (_data == null || _data.Gifts == null ||
                _data.Gifts.Count < DAYS_IN_WEEK || !_content)
                return;

            _usedFrames.Clear();
            _bars.Clear();

            for (var i = 0; i < DAYS_IN_WEEK - 1; i++)
            {
                var dayIndex = i + 1;
                var bar = Instantiate(_giftBarPrefab, _content);

                bar.Init(_data.Gifts[i], GetRandomFrame(), dayIndex);
                _bars.Add(bar);
            }

            var superBar = Instantiate(_superGiftBarPrefab, _content);

            superBar.Init(_data.Gifts[DAYS_IN_WEEK - 1], DAYS_IN_WEEK);
            _bars.Add(superBar);

            if (_contentDisplayer)
            {
                _contentDisplayer.InitTargets(
                    _bars.Select(bar => bar.transform).ToList()
                );
            }
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
                availableFrames = _frames.Where(frame => frame).ToList();
            }

            if (availableFrames.Count == 0)
                return null;

            var selectedFrame = availableFrames[Random.Range(0, availableFrames.Count)];

            _usedFrames.Add(selectedFrame);

            return selectedFrame;
        }

        private void Refresh()
        {
            foreach (var bar in _bars.Where(bar => bar))
            {
                bar.UpdateVisual();
            }
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

            if (_contentDisplayer)
                _contentDisplayer.Play();

            if (_sliderGifts)
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
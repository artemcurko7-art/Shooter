
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.DailyGift
{
    public class DailyGiftBar : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _rays;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _darkFrame;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private GameObject _crossMark;
        [SerializeField] private TMP_Text _day;
        [SerializeField] private TMP_Text _count;

        private int _dayIndex;
        private DailyGiftData.DailyGift _dailyGift;

        public void Init(DailyGiftData.DailyGift dailyGift, int dayIndex)
        {
            _dailyGift = dailyGift;
            _dayIndex = dayIndex;

            UpdateRewardVisual();
        }

        public void Init(DailyGiftData.DailyGift dailyGift, Sprite frame, int dayIndex)
        {
            _dailyGift = dailyGift;
            _dayIndex = dayIndex;

            if (_frame)
                _frame.sprite = frame;

            UpdateRewardVisual();
        }

        private void UpdateRewardVisual()
        {
            if (_dailyGift?.Reward == null)
            {
                Debug.LogWarning("DailyGiftBar: Daily gift or reward is missing.", this);
                return;
            }

            var reward = _dailyGift.Reward;
            var count = reward.Count;

            if (_day)
                _day.text = $"{GetLocalizedDay(YG2.lang)} {_dayIndex}";

            if (_icon)
                _icon.sprite = reward.Icon;

            if (_count)
            {
                _count.text = count.ToString();
                _count.enabled = count > 0;
            }
        }

        private static string GetLocalizedDay(string languageCode)
        {
            return languageCode switch
            {
                "ru" => "День",
                "en" => "Day",
                "tr" => "Gün",
                _ => "Day"
            };
        }

        public void UpdateVisual()
        {
            var currentDay = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;
            var isTaken = YG2.saves.TakenDailyGiftDays.Contains(_dayIndex);
            var isToday = _dayIndex == currentDay;
            var isPast = _dayIndex < currentDay;
            var isFuture = _dayIndex > currentDay;

            if (_rays)
                _rays.SetActive(isToday && !isTaken);

            if (_checkMark)
                _checkMark.SetActive(isTaken);

            if (_crossMark)
                _crossMark.SetActive(isPast && !isTaken);

            if (_darkFrame)
                _darkFrame.SetActive(isFuture || (isPast && !isTaken));

            if (_lock)
                _lock.SetActive(isFuture);
        }
    }
}

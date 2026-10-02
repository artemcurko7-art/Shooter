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

            _day.text = $"{GetLocalizedDay(YG2.lang)} {dayIndex}";
            _icon.sprite = dailyGift.icon;

            _count.enabled = dailyGift.count > 0;
        }

        public void Init(DailyGiftData.DailyGift dailyGift, Sprite frame, int dayIndex)
        {
            _dailyGift = dailyGift;
            _dayIndex = dayIndex;

            _day.text = $"{GetLocalizedDay(YG2.lang)} {dayIndex}";
            _icon.sprite = dailyGift.icon;
            _count.text = dailyGift.count.ToString();
            _frame.sprite = frame;
        }

        private static string GetLocalizedDay(string languageCode)
        {
            return languageCode switch
            {
                "ru" => "День",
                "en" => "Day",
                "tr" => "gün",
                _ => "Day",
            };
        }

        public void UpdateVisual()
        {
            var currentDay = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;
            var isTaken = YG2.saves.TakenDailyGiftDays.Contains(_dayIndex);
            var isToday = _dayIndex == currentDay;
            var isPast = _dayIndex < currentDay;
            var isFuture = _dayIndex > currentDay;

            _rays.SetActive(isToday && !isTaken);
            _checkMark.SetActive(isTaken);
            _crossMark.SetActive(isPast && !isTaken);

            _darkFrame.SetActive(isFuture || (isPast && !isTaken));
            _lock.SetActive(isFuture);
        }
    }
}
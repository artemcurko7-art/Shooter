using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.DailyReward
{
    public class DailyGiftBar : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _rays;
        [SerializeField] private GameObject _darkFrame;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private TMP_Text _day;
        [SerializeField] private TMP_Text _count;

        private DailyGiftData.DailyGift _dailyGift;

        public void Init(DailyGiftData.DailyGift dailyGift, Sprite frame, int dayIndex, bool isAvailable, bool isTaken)
        {
            _dailyGift = dailyGift;
            _day.text = $"{GetLocalizedDay(YG2.lang)} {dayIndex}";
            _icon.sprite = dailyGift.icon;
            _count.text = dailyGift.count.ToString();
            _frame.sprite = frame;

            _darkFrame.SetActive(!isAvailable);
            _checkMark.SetActive(isTaken);
        }

        public void Init(DailyGiftData.DailyGift dailyGift, int dayIndex, bool isAvailable, bool isTaken)
        {
            _dailyGift = dailyGift;
            _day.text = $"{GetLocalizedDay(YG2.lang)} {dayIndex}";
            _icon.sprite = dailyGift.icon;

            if (dailyGift.count == 0) _count.enabled = false;
            _darkFrame.SetActive(!isAvailable);
            _checkMark.SetActive(isTaken);
            _rays.SetActive(!isTaken);
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
    }
}
using Game.Scripts.UI.DailyReward;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Gifts
{
    public class GiftBar : MonoBehaviour
    {
        private const int AVAILABLE_GIFT_COUNT = 5;
        
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _rays;
        [SerializeField] private GameObject _darkFrame;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private TMP_Text _day;
        [SerializeField] private TMP_Text _count;
        
        
        public void Init(DailyGiftData.DailyGift dailyGift, bool isAvailable, bool isTaken)
        {
            _icon.sprite = dailyGift.icon;
            _count.text = dailyGift.count.ToString();

            _darkFrame.SetActive(!isAvailable);
            _checkMark.SetActive(isTaken);
        }
    }
}

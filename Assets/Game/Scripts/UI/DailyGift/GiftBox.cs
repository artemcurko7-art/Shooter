using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.DailyGift
{
    public class GiftBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _rays;
        [SerializeField] private RectTransform _checkMark;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        private SliderGifts.GiftDay _giftDay;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _giftDay.Collect();
            UpdateVisible(true, true);
        }

        public void UpdateVisible(bool isActive, bool isCollected)
        {
            _rays.gameObject.SetActive(isActive && !isCollected);
            _button.enabled = isActive && !isCollected;
            _checkMark.gameObject.SetActive(isActive && isCollected);
        }

        public void Init(SliderGifts.GiftDay giftDay)
        {
            _giftDay = giftDay;
        }
    }
}
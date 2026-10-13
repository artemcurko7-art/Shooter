using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    [RequireComponent(typeof(Button))]
    public class BuyItemBar : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _price;

        private Shop _shop;
        private bool _isTaken;
        private Button _button;
        private BuyItemData.BuyItem _buyItem;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_button != null)
                _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            if (_button != null)
                _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Init(Shop shop, BuyItemData.BuyItem buyItem, bool isTaken)
        {
            if (shop == null || buyItem == null)
                return;

            _shop = shop;
            _buyItem = buyItem;
            _isTaken = isTaken;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_buyItem == null)
                return;

            var reward = _buyItem.Reward;

            if (_icon != null)
                _icon.sprite = reward?.Icon;

            if (_count != null)
            {
                _count.text = reward != null && reward.Count > 0
                    ? reward.Count.ToString()
                    : string.Empty;
            }

            if (_price != null)
                _price.text = _buyItem.Price > 0 ? $"${_buyItem.Price}" : string.Empty;

            if (_checkMark != null)
                _checkMark.SetActive(_isTaken);
        }

        private void OnButtonClick()
        {
            if (_shop == null || _buyItem == null)
                return;

            if (_shop.IsPreviewAnimating)
                return;

            _shop.OpenPreview(_buyItem, transform.position);
        }
    }
}
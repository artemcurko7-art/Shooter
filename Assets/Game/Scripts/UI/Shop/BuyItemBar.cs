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
        private BuyItemData _data;
        private BuyItemData.BuyItem _buyItem;
        private bool _isTaken;
        private Button _button;

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

        public void Init(Shop shop, BuyItemData data, BuyItemData.BuyItem buyItem, bool isTaken)
        {
            if (shop == null || data == null || buyItem == null)
                return;

            var definition = data.GetDefinition(buyItem);

            if (!buyItem.IsValid(definition))
                return;

            _shop = shop;
            _data = data;
            _buyItem = buyItem;
            _isTaken = isTaken;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_data == null || _buyItem == null)
                return;

            var definition = _data.GetDefinition(_buyItem);

            if (!_buyItem.IsValid(definition))
                return;

            if (_icon != null)
                _icon.sprite = _buyItem.GetIcon(definition);

            if (_count != null)
            {
                var count = _buyItem.GetCount(definition);
                _count.text = count > 1 ? count.ToString() : string.Empty;
            }

            if (_price != null)
            {
                var price = _buyItem.Price;
                _price.text = price > 0 ? $"${price}" : string.Empty;
            }

            if (_checkMark != null)
                _checkMark.SetActive(_isTaken);
        }

        private void OnButtonClick()
        {
            if (_shop == null || _data == null || _buyItem == null)
                return;

            if (_shop.IsPreviewAnimating)
                return;

            _shop.OpenPreview(_data, _buyItem, transform.position);
        }
    }
}
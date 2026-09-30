using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class BuyItemBar : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _price;

        private bool _isTaken;
        private BuyItemData.BuyItem _buyItem;

        public void Init(BuyItemData.BuyItem buyItem, bool isTaken)
        {
            _buyItem = buyItem;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_buyItem == null) return;
            if (_buyItem.icon != null) _icon.sprite = _buyItem.icon;
            if (_count != null) _count.text = _buyItem.count > 0 ? _buyItem.count.ToString() : string.Empty;
            if (_price != null) _price.text = _buyItem.price > 0 ? '$' + _buyItem.price.ToString() : string.Empty;
            if (_checkMark != null) _checkMark.SetActive(_isTaken);
        }
    }
}
using System;
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
            _button.onClick.AddListener(OnButtonClock);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClock);
        }

        public void Init(Shop shop, BuyItemData.BuyItem buyItem, bool isTaken)
        {
            _shop = shop;
            _buyItem = buyItem;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_buyItem == null) return;
            if (_buyItem.icon) _icon.sprite = _buyItem.icon;
            if (_count) _count.text = _buyItem.count > 0 ? _buyItem.count.ToString() : string.Empty;
            if (_price) _price.text = _buyItem.price > 0 ? '$' + _buyItem.price.ToString() : string.Empty;
            if (_checkMark) _checkMark.SetActive(_isTaken);
        }

        private void OnButtonClock()
        {
            if (!_shop || _buyItem == null) return;
            Debug.Log("OpenPreview");


            _shop.OpenPreview(_buyItem, transform.position);
        }
    }
}
using System;
using Game.Scripts.Genetic;
using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Shop
{
    public class ShopPreview : Window
    {
        [Header("Зависимости")]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyItemButton;

        private ResourceType? _resourceType;

        protected override void OnEnable()
        {
            base.OnEnable();
            _buyItemButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _buyItemButton.onClick.RemoveListener(OnBuyButtonClick);
            Close();
        }

        public void Open(BuyItemData.BuyItem buyItem, Vector3 startPosition)
        {
            //if (_rectTransform.gameObject.activeInHierarchy) return;

            _resourceType = buyItem.resource;
            _icon.sprite = buyItem.icon;
            _title.text = Localization.GetNameTranslations(buyItem.resource);
            _buyButtonText.text = Localization.GetBuyText();

            _transition.Open(_canvasGroup, _rectTransform, startPosition, _scaleEase, _positionEase, _duration);
        }

        private void OnBuyButtonClick()
        {
            Close();
        }

        private void Close()
        {
            _transition.Close(_canvasGroup, _rectTransform);
            _resourceType = null;
        }

        protected override void Show()
        {
            throw new NotImplementedException();
        }

        protected override void Hide()
        {
            Close();
        }
    }
}
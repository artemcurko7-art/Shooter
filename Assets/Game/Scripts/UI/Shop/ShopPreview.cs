using Game.Scripts.UI.Animation;
using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class ShopPreview : Preview
    {
        [Header("Зависимости")]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private PopEffect _effect;

        private Vector3 _startPosition;
        private BuyItemData _data;
        private BuyItemData.BuyItem _buyItem;

        public bool IsEffectAnimating => _effect && _effect.IsAnimating;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_buyItemButton != null)
                _buyItemButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_buyItemButton != null)
                _buyItemButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        public void Open(BuyItemData data, BuyItemData.BuyItem buyItem, Vector3 startPosition)
        {
            if (IsEffectAnimating)
                return;

            if (data == null || buyItem == null)
            {
                Debug.LogWarning("[ShopPreview] BuyItemData или BuyItem — null.", this);
                return;
            }

            var definition = data.GetDefinition(buyItem);
            if (!buyItem.IsValid(definition))
            {
                Debug.LogWarning("[ShopPreview] Определение предмета невалидно.", this);
                return;
            }

            if (_transition == null || _canvasGroup == null || _rectTransform == null)
            {
                Debug.LogWarning("[ShopPreview] Не хватает зависимостей для анимации.", this);
                return;
            }

            _data = data;
            _buyItem = buyItem;
            _startPosition = startPosition;

            UpdateDisplay(definition);

            if (_buyButtonText)
                _buyButtonText.text = Localization.GetBuyText();

            OpenPreview(_startPosition);
        }

        protected override void Show(Vector3 startPosition)
        {
            if (!_transition || !_canvasGroup || !_rectTransform)
            {
                FinishTransition();
                return;
            }

            _transition.Open(_canvasGroup, _rectTransform, startPosition,
                _scaleEase, _positionEase, _duration, FinishTransition);
        }

        protected override void Hide(bool playEffect)
        {
            CloseInternal(playEffect);
        }

        private void CloseInternal(bool playEffect)
        {
            var definition = _data != null && _buyItem != null
                ? _data.GetDefinition(_buyItem)
                : null;

            var icon = playEffect ? _buyItem?.GetIcon(definition) : null;
            var count = playEffect ? _buyItem?.GetCount(definition) : null;

            _buyItem = null;
            _data = null;

            if (playEffect && _effect && icon)
            {
                Debug.Log("[ShopPreview] Запускаем эффект покупки перед закрытием.");
                _effect.Animate(icon, count ?? 0, true, _startPosition);
            }

            if (_transition == null || _canvasGroup == null || _rectTransform == null)
            {
                FinishTransition();
                return;
            }

            _transition.Close(_canvasGroup, _rectTransform, FinishTransition);
        }

        private void UpdateDisplay(RewardDefinition definition)
        {
            var count = _buyItem.GetCount(definition);
            var price = _buyItem.Price;
            var icon = _buyItem.GetIcon(definition);
            var title = _buyItem.GetLocalizedName(definition);

            if (_count != null)
                _count.text = count.ToString();

            if (_price != null)
                _price.text = price > 0 ? $"${price}" : string.Empty;

            if (_icon != null)
                _icon.sprite = icon;

            if (_title != null)
                _title.text = title;
        }

        private void OnBuyButtonClick()
        {
            Debug.Log("[ShopPreview] Нажата кнопка «Купить».");

            if (_data == null || _buyItem == null || _effect == null)
            {
                Debug.LogWarning("[ShopPreview] Нет данных или эффекта для анимации.", this);
                ClosePreview();
                return;
            }

            var definition = _data.GetDefinition(_buyItem);
            if (!definition || !_buyItem.IsValid(definition))
            {
                Debug.LogWarning("[ShopPreview] Предмет невалиден при покупке.", this);
            }

            ClosePreview();
        }
    }
}
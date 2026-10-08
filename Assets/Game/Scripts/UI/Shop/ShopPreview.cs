using System;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class ShopPreview : Window
    {
        [Header("Зависимости")]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private BuyEffect _effect;

        private Vector3 _startPosition;
        private BuyItemData.BuyItem _buyItem;

        public bool IsEffectAnimating => _effect.IsAnimating;

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

        public void Open(BuyItemData.BuyItem buyItem, Vector3 startPosition)
        {
            if (_effect.IsAnimating) return;

            if (buyItem == null)
            {
                Debug.LogWarning("ShopPreview: BuyItem is NULL.", this);
                return;
            }

            if (_transition == null)
            {
                Debug.LogWarning("ShopPreview: Transition is NULL.", this);
                return;
            }

            if (_canvasGroup == null)
            {
                Debug.LogWarning("ShopPreview: CanvasGroup is NULL.", this);
                return;
            }

            if (_rectTransform == null)
            {
                Debug.LogWarning("ShopPreview: RectTransform is NULL.", this);
                return;
            }

            Debug.Log("ShopPreview: starting transition.", this);

            _buyItem = buyItem;
            _count.text = _buyItem.count.ToString();
            _startPosition = startPosition;

            if (_icon != null)
                _icon.sprite = buyItem.icon;

            if (_title != null)
                _title.text = Localization.GetNameTranslations(buyItem.resource);

            if (_buyButtonText != null)
                _buyButtonText.text = Localization.GetBuyText();

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                startPosition,
                _scaleEase,
                _positionEase,
                _duration);
        }

        private void OnBuyButtonClick()
        {
            if (_buyItem == null) return;

            _effect.Animate(_buyItem.icon, _buyItem.count, _startPosition);
            Close();
        }

        private void Close()
        {
            _buyItem = null;

            if (_transition == null || _canvasGroup == null || _rectTransform == null)
                return;

            _transition.Close(_canvasGroup, _rectTransform);
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
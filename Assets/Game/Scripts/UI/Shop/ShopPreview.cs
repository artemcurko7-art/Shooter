using Game.Scripts.UI.Animation;
using Game.Scripts.UI.TabContext;
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
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private BuyEffect _effect;

        private Vector3 _startPosition;
        private BuyItemData.BuyItem _buyItem;

        public bool IsEffectAnimating => _effect != null && _effect.IsAnimating;

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
            if (IsEffectAnimating)
                return;

            if (buyItem == null || buyItem.Reward == null)
            {
                Debug.LogWarning("ShopPreview: BuyItem or Reward is NULL.", this);
                return;
            }

            if (_transition == null || _canvasGroup == null || _rectTransform == null)
            {
                Debug.LogWarning("ShopPreview: Transition dependencies are NULL.", this);
                return;
            }

            _buyItem = buyItem;
            _startPosition = startPosition;

            var reward = _buyItem.Reward;

            if (_count != null)
                _count.text = reward.Count.ToString();

            if (_price != null)
                _price.text = '$' + _buyItem.Price.ToString();

            if (_icon != null)
                _icon.sprite = reward.Icon;

            if (_title != null)
                _title.text = Localization.GetNameTranslations(_buyItem.Reward.Item.Type);

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
            if (_buyItem == null || _buyItem.Reward == null || _effect == null)
                return;

            _effect.Animate(
                _buyItem.Reward.Icon,
                _buyItem.Reward.Count,
                _startPosition);

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
            if (_buyItem == null)
                return;

            Open(_buyItem, _startPosition);
        }

        protected override void Hide()
        {
            Close();
        }
    }
}
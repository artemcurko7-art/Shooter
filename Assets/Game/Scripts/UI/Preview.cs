using Game.Scripts.Genetic;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI
{
    public class Preview : Window
    {
        [Header("Зависимости")]
        [SerializeField] private GeneticSystem _geneticSystem;
        [SerializeField] private ImageBlicker _imageBlicker;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RawImage _background;
        [SerializeField] private Image _iconFrame;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _geneticTitle;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyStatButton;
        [SerializeField] private TMP_Text _originStatValue;
        [SerializeField] private TMP_Text _targetStatValue;

        private StatsData.Stat _stat;

        protected override void OnEnable()
        {
            base.OnEnable();
            _buyStatButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _buyStatButton.onClick.RemoveListener(OnBuyButtonClick);
            Close();
        }

        public void Open(StatsData.Stat stat, Vector3 startPosition)
        {
            if (_rectTransform.gameObject.activeInHierarchy) return;

            _stat = stat;
            _icon.sprite = stat.icon;
            _title.text = stat.GetLocalizedName(YG2.lang);
            _geneticTitle.text = Localization.GetGeneticTitleText();
            _buyButtonText.text = Localization.GetLocalizedBuyText();
            _background.color = Color.grey;
            _scrollRect.enabled = false;

            var originValue = GeneticSystem.GetStatValue(_stat.name);
            var targetValue = originValue + _geneticSystem.IncreaseNumber;

            _originStatValue.text = $"{originValue}+";
            _targetStatValue.text = $"{targetValue}+";

            var canBuy = _buyStatButton.interactable;

            _transition.Open(_canvasGroup, _rectTransform, startPosition, _scaleEase, _positionEase, _duration);

            if (!_imageBlicker) return;

            _imageBlicker.ResetToBaseColor();

            if (canBuy)
            {
                _imageBlicker.Enable();
            }
            else
            {
                _imageBlicker.Disable();
            }
        }
        
        private void OnBuyButtonClick()
        {
            if (_stat == null)
            {
                Debug.LogError("[Preview] _stat не передается в open!");
                return;
            }

            _geneticSystem.IncreaseStat(_stat.name);
            Close();
        }

        private void Close()
        {
            if (_imageBlicker)
            {
                _imageBlicker.Disable();
            }

            _background.color = Color.white;
            _scrollRect.enabled = true;
            _transition.Close(_canvasGroup, _rectTransform);
            _stat = null;
        }

        protected override void Show()
        {
            throw new System.NotImplementedException();
        }

        protected override void Hide()
        {
            Close();
        }
    }
}
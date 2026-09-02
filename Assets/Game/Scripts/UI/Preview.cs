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
        [SerializeField] private WindowTransition _transition;
        [SerializeField] private GeneticSystem _geneticSystem;
        [SerializeField] private ImageBlicker _imageBlicker;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RawImage _background;
        [SerializeField] private Image _iconFrame;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyStatButton;

        [Header("Настройки превью")]
        [SerializeField] private TMP_Text _originStatValue;
        [SerializeField] private TMP_Text _targetStatValue;

        private StatsData.Stat _stat;

        private void OnEnable()
        {
            _buyStatButton.onClick.AddListener(OnBuyButtonClick);
        }

        private void OnDisable()
        {
            _buyStatButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        public void Open(StatsData.Stat stat, Vector3 startPosition)
        {
            _stat = stat;
            _icon.sprite = stat.icon;
            _title.text = stat.GetLocalizedName(YG2.lang);
            _buyButtonText.text = GetLocalizedBuyText();
            _background.color = Color.grey;
            _scrollRect.enabled = false;

            var originValue = GeneticSystem.GetStatValue(_stat.name);
            var targetValue = originValue + _geneticSystem.IncreaseNumber;

            _originStatValue.text = $"{originValue}+";
            _targetStatValue.text = $"{targetValue}+";

            var canBuy = _buyStatButton.interactable;
            if (_imageBlicker)
            {
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

            _transition.Init(_canvasGroup, _rectTransform);
            _transition.Open(startPosition, _scaleEase, _positionEase, _duration);
        }

        private static string GetLocalizedBuyText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Получить!",
                "en" => "Receive!",
                "tr" => "almakt?r",
                _ => "Receive!",
            };
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
            _transition.Close();
            _stat = null;
        }

        protected override void Show()
        {
            throw new System.NotImplementedException();
        }

        protected override void Hide()
        {
            if (IsTransitionActive) return;

            Close();
        }
    }
}
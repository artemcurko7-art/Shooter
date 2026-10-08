using Game.Scripts.Genetic;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticPreview : Window
    {
        [Header("Зависимости")]
        [SerializeField] private GeneticSystem _geneticSystem;
        [SerializeField] private ImageBlinker _imageBlinker;
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

            if (_buyStatButton)
                _buyStatButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_buyStatButton)
                _buyStatButton.onClick.RemoveListener(OnBuyButtonClick);

            CloseImmediate();
        }

        public void Open(StatsData.Stat stat, Vector3 startPosition)
        {
            if (!TryBeginOpen())
                return;

            if (stat == null)
            {
                FinishTransition();
                return;
            }

            _stat = stat;

            _icon.sprite = stat.icon;
            _title.text = stat.GetLocalizedName(YG2.lang);
            _geneticTitle.text = Localization.GetGeneticTitleText();
            _buyButtonText.text = Localization.GetUpgradeText();

            _background.color = Color.grey;
            _scrollRect.enabled = false;

            var originValue = GeneticSystem.GetStatValue(_stat.name);
            var targetValue = originValue + _geneticSystem.IncreaseNumber;

            _originStatValue.text = $"{originValue}+";
            _targetStatValue.text = $"{targetValue}+";

            var canBuy = _buyStatButton.interactable;

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                startPosition,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition
            );

            if (!_imageBlinker)
                return;

            _imageBlinker.ResetToBaseColor();

            if (canBuy)
                _imageBlinker.Enable();
            else
                _imageBlinker.Disable();
        }

        private void OnBuyButtonClick()
        {
            if (_stat == null)
            {
                Debug.LogError("[Preview] _stat не передается в Open!");
                return;
            }

            _geneticSystem.IncreaseStat(_stat.name);
            Close();
        }

        private void Close()
        {
            if (!TryBeginClose())
                return;

            if (_imageBlinker)
                _imageBlinker.Disable();

            _background.color = Color.white;
            _scrollRect.enabled = true;

            _transition.Close(
                _canvasGroup,
                _rectTransform,
                FinishTransition
            );

            _stat = null;
        }

        private void CloseImmediate()
        {
            _stat = null;

            if (_imageBlinker)
                _imageBlinker.Disable();

            if (_background)
                _background.color = Color.white;

            if (_scrollRect)
                _scrollRect.enabled = true;

            if (_canvasGroup && _rectTransform)
            {
                _transition.QuickClose(
                    _canvasGroup,
                    _rectTransform
                );
            }
        }

        protected override void Show()
        {
        }

        protected override void Hide()
        {
            Close();
        }
    }
}
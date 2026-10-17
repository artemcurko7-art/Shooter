using Game.Scripts.Genetic;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticPreview : Preview
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

            if (_buyStatButton != null)
                _buyStatButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_buyStatButton != null)
                _buyStatButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        public void Open(StatsData.Stat stat, Vector3 startPosition)
        {
            if (stat == null)
            {
                Debug.LogError("[GeneticPreview] Передан null вместо стата.", this);
                return;
            }

            _stat = stat;

            _icon.sprite = stat.icon;
            _title.text = stat.GetLocalizedName(YG2.lang);
            _geneticTitle.text = Localization.GetGeneticTitleText();
            _buyButtonText.text = Localization.GetUpgradeText();

            _background.color = Color.grey;
            if (_scrollRect != null)
                _scrollRect.enabled = false;

            var originValue = GeneticSystem.GetStatValue(_stat.name);
            var targetValue = originValue + _geneticSystem.IncreaseNumber;

            _originStatValue.text = $"{originValue}+";
            _targetStatValue.text = $"{targetValue}+";

            bool canBuy = _buyStatButton.interactable;

            if (_imageBlinker != null)
            {
                _imageBlinker.ResetToBaseColor();
                if (canBuy)
                    _imageBlinker.Enable();
                else
                    _imageBlinker.Disable();
            }

            OpenPreview(startPosition);
        }

        protected override void Show(Vector3 startPosition)
        {
            if (_transition == null || _canvasGroup == null || _rectTransform == null)
            {
                FinishTransition();
                return;
            }

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                startPosition,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition);
        }

        protected override void Hide()
        {
            if (_stat == null)
            {
                // Даже если стат потерялся, всё равно закрываем анимацией
                FinishTransition();
                return;
            }

            if (_imageBlinker != null)
                _imageBlinker.Disable();

            if (_background != null)
                _background.color = Color.white;

            if (_scrollRect != null)
                _scrollRect.enabled = true;

            var statName = _stat.name;
            _stat = null;

            if (_geneticSystem != null) _geneticSystem.IncreaseStat(statName);

            if (_transition && _canvasGroup && _rectTransform)
                _transition.Close(_canvasGroup, _rectTransform, FinishTransition);
            else
                FinishTransition();
        }

        private void OnBuyButtonClick()
        {
            Debug.Log("[GeneticPreview] Нажата кнопка «Улучшить».");
            ClosePreview();
        }
    }
}
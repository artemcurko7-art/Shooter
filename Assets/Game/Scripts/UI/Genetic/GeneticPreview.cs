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
        [SerializeField] private TMP_Text _openButtonText;
        [SerializeField] private Button _openButton;
        [SerializeField] private TMP_Text _originStatValue;
        [SerializeField] private TMP_Text _targetStatValue;

        private StatsData.Stat _stat;
        private int _statIndex = -1;
        private bool _isUpgradeConfirmed;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!_openButton) return;
            _openButton.onClick.AddListener(OnOpenButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_openButton)
                _openButton.onClick.RemoveListener(OnOpenButtonClick);
        }

        public void Open(StatsData.Stat stat, int index, Vector3 startPosition)
        {
            if (stat == null || !_geneticSystem)
            {
                Debug.LogWarning(
                    "[GeneticPreview] Не удалось открыть превью: проверьте характеристику и GeneticSystem.", this);
                return;
            }

            _stat = stat;
            _statIndex = index;
            _isUpgradeConfirmed = false;

            if (_icon)
                _icon.sprite = stat.icon;

            if (_title)
                _title.text = stat.GetLocalizedName(YG2.lang);

            if (_geneticTitle)
                _geneticTitle.text = Localization.GetGeneticTitleText();

            if (_openButtonText)
                _openButtonText.text = Localization.GetUpgradeText();

            if (_background)
                _background.color = Color.grey;

            if (_scrollRect)
                _scrollRect.enabled = false;

            var originValue = GeneticSystem.GetStatValue(stat);
            var targetValue = originValue + _geneticSystem.IncreaseNumber;

            if (_originStatValue)
                _originStatValue.text = $"{originValue}+";

            if (_targetStatValue)
                _targetStatValue.text = $"{targetValue}+";

            var canBuy = GeneticSystem.IsAvailableStat(index);

            if (_openButton)
                _openButton.interactable = canBuy;

            if (_imageBlinker)
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
            if (!_transition || !_canvasGroup || !_rectTransform)
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


        protected override void Hide(bool playEffect)
        {
            if (_imageBlinker)
                _imageBlinker.Disable();

            if (_background)
                _background.color = Color.white;

            if (_scrollRect)
                _scrollRect.enabled = true;

            _stat = null;
            _statIndex = -1;
            _isUpgradeConfirmed = false;

            if (_transition && _canvasGroup && _rectTransform)
                _transition.Close(_canvasGroup, _rectTransform, FinishTransition);
            else
                FinishTransition();
        }


        private void OnOpenButtonClick()
        {
            if (_isUpgradeConfirmed || _stat == null || !_geneticSystem)
                return;

            if (!GeneticSystem.IsAvailableStat(_statIndex))
            {
                Debug.LogWarning(
                    $"[GeneticPreview] Характеристика с индексом {_statIndex} больше недоступна.",
                    this);

                return;
            }

            _isUpgradeConfirmed = true;

            var stat = _stat;
            var index = _statIndex;

            if (_openButton)
                _openButton.interactable = false;

            _geneticSystem.IncreaseStat(stat, index);
            ClosePreview();
        }
    }
}
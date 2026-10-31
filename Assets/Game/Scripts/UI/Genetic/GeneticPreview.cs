using Game.Scripts.UI.Animation;
using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticPreview : Preview
    {
        private enum PreviewType
        {
            Stat,
            Addition
        }

        [Header("Зависимости")]
        [SerializeField] private GeneticSystem _geneticSystem;
        [SerializeField] private ImageBlinker _imageBlinker;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RawImage _background;
        [SerializeField] private PopEffect _effect;
        [SerializeField] private RectTransform _statTextBar;
        [SerializeField] private RectTransform _additionTextBar;

        [Header("Иконки")]
        [SerializeField] private RectTransform _additionIconBar;
        [SerializeField] private RectTransform _statIconBar;
        [SerializeField] private Image _statIcon;
        [SerializeField] private Image _additionIcon;

        [Header("Текст")]
        [SerializeField] private TMP_Text _itemTitle;
        [SerializeField] private TMP_Text _mainTitle;
        [SerializeField] private TMP_Text _openButtonText;
        [SerializeField] private TMP_Text _takeButtonText;
        [SerializeField] private TMP_Text _originStatValue;
        [SerializeField] private TMP_Text _targetStatValue;
        [SerializeField] private TMP_Text _additionCountText;

        [Header("Кнопки")]
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _takeButton;

        private PreviewType _previewType;
        private StatsData.Stat _stat;
        private RewardDefinition _addition;
        private int _statIndex = -1;
        private int _count;
        private bool _isUpgradeConfirmed;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_openButton)
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
            OpenInternal(stat, null, index, 0, startPosition);
        }

        public void Open(RewardDefinition addition, int count, Vector3 startPosition)
        {
            OpenInternal(null, addition, -1, count, startPosition);
        }

        private void OpenInternal(
            StatsData.Stat stat,
            RewardDefinition addition,
            int index,
            int count,
            Vector3 startPosition)
        {
            if (stat == null && !addition)
            {
                Debug.LogWarning(
                    "[GeneticPreview] Не передана характеристика или дополнение.",
                    this);
                return;
            }

            if (stat != null && !_geneticSystem)
            {
                Debug.LogWarning("[GeneticPreview] Не назначен GeneticSystem.", this);
                return;
            }

            var isStat = stat != null;

            _previewType = isStat ? PreviewType.Stat : PreviewType.Addition;
            _stat = stat;
            _addition = addition;
            _statIndex = index;
            _count = count;
            _isUpgradeConfirmed = false;

            if (_statIcon)
                _statIcon.sprite = isStat ? stat.icon : addition.GetIcon(count);

            if (_additionIcon)
            {
                _additionIcon.sprite = isStat ? null : addition.GetIcon(count);
                _additionIcon.gameObject.SetActive(!isStat);
            }

            if (_itemTitle)
            {
                _itemTitle.text = isStat
                    ? stat.GetLocalizedName(YG2.lang)
                    : addition.GetLocalizedName();
            }

            if (_mainTitle)
            {
                _mainTitle.gameObject.SetActive(true);
                _mainTitle.text = isStat ? Localization.GetGeneticTitleText() : Localization.GetRewardTitleText();
            }

            if (_openButtonText && isStat)
                _openButtonText.text = Localization.GetUpgradeText();

            var originValue = isStat ? GeneticSystem.GetStatValue(stat) : 0f;

            if (_originStatValue)
            {
                _originStatValue.gameObject.SetActive(isStat);

                if (isStat)
                    _originStatValue.text = $"{originValue}+";
            }

            if (_targetStatValue)
            {
                _targetStatValue.gameObject.SetActive(isStat);

                if (isStat)
                {
                    var targetValue = originValue + _geneticSystem.IncreaseNumber;
                    _targetStatValue.text = $"{targetValue}+";
                }
            }

            if (_additionCountText)
            {
                _additionCountText.gameObject.SetActive(!isStat);

                if (!isStat)
                    _additionCountText.text = count.ToString();
            }

            SetBarActive(_statTextBar, isStat);
            SetBarActive(_additionTextBar, !isStat);
            SetBarActive(_statIconBar, isStat);
            SetBarActive(_additionIconBar, !isStat);

            var canUpgrade = isStat && GeneticSystem.IsAvailableStat(index);
            var canTake = !isStat;

            if (_openButton)
            {
                _openButton.gameObject.SetActive(isStat);
                _openButton.enabled = canUpgrade;
                _openButton.colors = StatusColors.GetColorBlock(canUpgrade);
            }

            if (_takeButton)
            {
                _takeButton.gameObject.SetActive(!isStat);
                _takeButton.enabled = canTake;
                _takeButton.colors = StatusColors.GetColorBlock(canUpgrade);
                ;
            }

            if (_imageBlinker)
            {
                _imageBlinker.Disable();

                if (canUpgrade)
                {
                    _imageBlinker.gameObject.SetActive(true);
                    _imageBlinker.Enable();
                }
            }

            PrepareAndOpen(startPosition);
        }

        private static void SetBarActive(RectTransform bar, bool isActive)
        {
            if (bar)
                bar.gameObject.SetActive(isActive);
        }

        private void PrepareAndOpen(Vector3 startPosition)
        {
            if (_background)
                _background.color = Color.grey;

            if (_scrollRect)
                _scrollRect.enabled = false;

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
            _addition = null;
            _statIndex = -1;
            _isUpgradeConfirmed = false;

            if (_additionCountText)
                _additionCountText.gameObject.SetActive(false);

            if (_additionIcon)
                _additionIcon.gameObject.SetActive(false);

            SetBarActive(_statTextBar, true);
            SetBarActive(_additionTextBar, false);
            SetBarActive(_statIconBar, true);
            SetBarActive(_additionIconBar, false);

            if (_mainTitle)
                _mainTitle.gameObject.SetActive(true);

            if (_originStatValue)
                _originStatValue.gameObject.SetActive(true);

            if (_targetStatValue)
                _targetStatValue.gameObject.SetActive(true);

            if (_openButton)
            {
                _openButton.gameObject.SetActive(true);
                _openButton.interactable = true;
            }

            if (_transition && _canvasGroup && _rectTransform)
            {
                _transition.Close(
                    _canvasGroup,
                    _rectTransform,
                    FinishTransition);
            }
            else
            {
                FinishTransition();
            }
        }

        private void OnOpenButtonClick()
        {
            if (_previewType != PreviewType.Stat ||
                _isUpgradeConfirmed ||
                _stat == null ||
                !_geneticSystem)
                return;

            if (!GeneticSystem.IsAvailableStat(_statIndex))
            {
                Debug.LogWarning(
                    $"[GeneticPreview] Характеристика с индексом {_statIndex} недоступна.",
                    this);
                return;
            }

            _isUpgradeConfirmed = true;

            var stat = _stat;
            var index = _statIndex;

            if (_openButton)
                _openButton.enabled = false;

            _geneticSystem.IncreaseStat(stat, index);

            var icon = _previewType == PreviewType.Stat ? _stat.icon : _addition.GetIcon(_count);
            _effect.Animate(icon, _count, true, _statIcon.transform.position);

            ClosePreview();
        }
    }
}
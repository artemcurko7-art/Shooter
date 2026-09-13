using System;
using DG.Tweening;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Challenges
{
    public class AchieveBar : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _descriptionBackground;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _checkMark;

        [Header("Спрайты")]
        [SerializeField] private Sprite _redBackgroundSprite;
        [SerializeField] private Sprite _greenBackgroundSprite;
        [SerializeField] private Sprite _redFrameSprite;
        [SerializeField] private Sprite _greenFrameSprite;

        [Header("Текст")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Color _redColor;
        [SerializeField] private Color _greenColor;
        [SerializeField] private TextAppear _textAppear;

        [Header("Кнопки")]
        [SerializeField] private Button _barButton;
        [SerializeField] private Button _closeButton;

        [Header("Анимация описания")]
        [SerializeField] private float _expandHeight = 1500f;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private VerticalLayoutGroup _layoutGroup;
        private Vector2 _originalDescSize;
        private Tween _descSizeTween;
        private bool _isOpened;

        private Action<RectTransform> _onClicked;
        private Action _onExpandComplete;
        private Action _onCloseComplete;

        public RectTransform RectTransform { get; private set; }
        public bool IsDescriptionExpanded { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();

            if (RectTransform)
                _originalDescSize = RectTransform.sizeDelta;

            if (!_barButton)
                _barButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_barButton)
                _barButton.onClick.AddListener(OnBarClicked);

            if (_closeButton)
                _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _descSizeTween?.Kill();

            if (_barButton)
                _barButton.onClick.RemoveListener(OnBarClicked);

            if (_closeButton)
                _closeButton.onClick.RemoveListener(OnCloseButtonClicked);

            if (_textAppear)
                _textAppear.Disable();
        }

        private void OnDestroy()
        {
            _descSizeTween?.Kill();
        }

        public void Init(
            AchievementData.Achieve achieve,
            ScrollRect scrollRect,
            Action<RectTransform> onClicked,
            Action onExpandComplete = null,
            Action onCloseComplete = null)
        {
            if (achieve == null)
                return;

            _onClicked = onClicked;
            _onExpandComplete = onExpandComplete;
            _onCloseComplete = onCloseComplete;
            _isOpened = achieve.isOpened;

            if (scrollRect && scrollRect.content)
                _layoutGroup = scrollRect.content.GetComponent<VerticalLayoutGroup>();

            if (_name)
                _name.text = achieve.GetLocalizedName(YG2.lang);

            if (_icon)
                _icon.sprite = achieve.icon;

            if (_description)
                _description.text = achieve.GetLocalizedDescription(YG2.lang);

            if (_checkMark)
                _checkMark.gameObject.SetActive(_isOpened);

            if (_lock)
                _lock.gameObject.SetActive(!_isOpened);

            if (_descriptionBackground)
            {
                _descriptionBackground.color = _isOpened ? _greenColor : _redColor;
                _descriptionBackground.gameObject.SetActive(false);
            }

            if (_background)
                _background.sprite = _isOpened ? _greenBackgroundSprite : _redBackgroundSprite;

            if (_frame)
                _frame.sprite = _isOpened ? _greenFrameSprite : _redFrameSprite;

            if (_closeButton)
                _closeButton.image.color = _isOpened ? _greenColor : _redColor;

            IsDescriptionExpanded = false;

            if (RectTransform)
                _originalDescSize = RectTransform.sizeDelta;

            SetOpenButtonEnabled(true);
        }

        public void SetOpenButtonEnabled(bool isEnabled)
        {
            if (_barButton)
                _barButton.enabled = isEnabled;
        }

        public void Expand()
        {
            if (!RectTransform || IsDescriptionExpanded)
                return;

            _descSizeTween?.Kill();

            IsDescriptionExpanded = true;
            SetOpenButtonEnabled(false);

            if (_descriptionBackground)
                _descriptionBackground.gameObject.SetActive(true);

            var targetSize = new Vector2(_originalDescSize.x, _originalDescSize.y + _expandHeight);

            _descSizeTween = RectTransform
                .DOSizeDelta(targetSize, _duration)
                .SetEase(_ease)
                .OnUpdate(() =>
                {
                    if (_layoutGroup)
                        LayoutRebuilder.MarkLayoutForRebuild(_layoutGroup.transform as RectTransform);
                })
                .OnComplete(() =>
                {
                    RebuildLayout();

                    _onExpandComplete?.Invoke();

                    if (_textAppear)
                        _textAppear.Enable();
                });
        }


        private void Close()
        {
            if (!RectTransform || !IsDescriptionExpanded)
                return;

            _descSizeTween?.Kill();

            _descSizeTween = RectTransform
                .DOSizeDelta(_originalDescSize, _duration)
                .SetEase(_ease)
                .OnUpdate(() =>
                {
                    if (_layoutGroup)
                    {
                        LayoutRebuilder.MarkLayoutForRebuild(
                            _layoutGroup.transform as RectTransform
                        );
                    }
                })
                .OnComplete(() =>
                {
                    IsDescriptionExpanded = false;

                    if (_textAppear)
                        _textAppear.Disable();

                    if (_descriptionBackground)
                        _descriptionBackground.gameObject.SetActive(false);

                    _onCloseComplete?.Invoke();
                });
        }

        private void RebuildLayout()
        {
            if (_layoutGroup)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(
                    _layoutGroup.transform as RectTransform
                );
            }

            Canvas.ForceUpdateCanvases();
        }

        private void OnBarClicked()
        {
            _onClicked?.Invoke(RectTransform);
        }

        private void OnCloseButtonClicked()
        {
            Close();
        }
    }
}
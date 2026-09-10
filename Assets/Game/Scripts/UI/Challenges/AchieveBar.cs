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
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _checkMark;

        [Header("Спрайты")]
        [SerializeField] private Sprite _redBackgroundSprite;
        [SerializeField] private Sprite _greenBackgroudSprite;
        [SerializeField] private Sprite _redFrameSprite;
        [SerializeField] private Sprite _greenFrameSprite;

        [Header("Текст")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TextAppear _textAppear;

        [Header("Кнопки")]
        [SerializeField] private Button _barButton;
        [SerializeField] private Button _closeButton;

        [Header("Анимация описания")]
        [SerializeField] private float _descExpandHeight = 60f;
        [SerializeField] private float _descDuration = 0.3f;
        [SerializeField] private Ease _descEase = Ease.OutQuad;

        private bool _isOpened;

        private VerticalLayoutGroup _layoutGroup;

        private Vector2 _originalDescSize;

        private Tween _descSizeTween;

        private Action<RectTransform> _onClicked;
        private Action _onExpandComplete;

        public bool IsDescExpanded { get; private set; }
        public RectTransform RectTransform { get; private set; }

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

            if (_textAppear)
                _textAppear.Disable();
        }

        private void OnDestroy()
        {
            _descSizeTween?.Kill();

            if (_barButton)
                _barButton.onClick.RemoveListener(OnBarClicked);

            if (_closeButton)
                _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void Init(AchievementData.Achieve achieve, ScrollRect scrollRect,
            Action<RectTransform> onClicked, Action onExpandComplete = null)
        {
            if (achieve == null)
                return;

            _onClicked = onClicked;
            _onExpandComplete = onExpandComplete;

            if (scrollRect && scrollRect.content)
            {
                _layoutGroup = scrollRect.content.GetComponent<VerticalLayoutGroup>();
            }

            if (_name)
                _name.text = achieve.GetLocalizedName(YG2.lang);

            if (_description)
                _description.text = achieve.GetLocalizedDescription(YG2.lang);

            if (_checkMark)
                _checkMark.gameObject.SetActive(_isOpened);

            if (_lock)
                _lock.gameObject.SetActive(!_isOpened);

            if (_background)
            {
                _background.sprite = _isOpened
                    ? _greenBackgroudSprite
                    : _redBackgroundSprite;
            }

            if (_frame)
            {
                _frame.sprite = _isOpened
                    ? _greenFrameSprite
                    : _redFrameSprite;
            }

            IsDescExpanded = false;

            if (RectTransform)
                _originalDescSize = RectTransform.sizeDelta;
        }

        public void Expand()
        {
            if (!RectTransform || IsDescExpanded)
                return;

            _descSizeTween?.Kill();

            IsDescExpanded = true;

            var pivot = RectTransform.pivot;

            var targetSize = new Vector2(
                _originalDescSize.x,
                _originalDescSize.y + _descExpandHeight
            );

            var currentSize = RectTransform.sizeDelta;
            var delta = targetSize - currentSize;

            var offsetY = -delta.y * (1f - pivot.y);
            var newPos = RectTransform.anchoredPosition + new Vector2(0f, offsetY);

            _descSizeTween = RectTransform
                .DOSizeDelta(targetSize, _descDuration)
                .SetEase(_descEase)
                .OnUpdate(() =>
                {
                    RectTransform.anchoredPosition = newPos;

                    if (_layoutGroup)
                    {
                        LayoutRebuilder.MarkLayoutForRebuild(
                            _layoutGroup.transform as RectTransform
                        );
                    }
                })
                .OnComplete(() =>
                {
                    Canvas.ForceUpdateCanvases();

                    if (_layoutGroup)
                    {
                        LayoutRebuilder.ForceRebuildLayoutImmediate(
                            _layoutGroup.transform as RectTransform
                        );
                    }

                    Canvas.ForceUpdateCanvases();

                    _onExpandComplete?.Invoke();
                });

            if (_textAppear)
                _textAppear.Enable();
        }

        public void Close()
        {
            if (!RectTransform || !IsDescExpanded)
                return;

            _descSizeTween?.Kill();

            IsDescExpanded = false;

            var pivot = RectTransform.pivot;
            var currentSize = RectTransform.sizeDelta;
            var backDelta = _originalDescSize - currentSize;

            var backOffsetY = -backDelta.y * (1f - pivot.y);
            var backPos = RectTransform.anchoredPosition + new Vector2(0f, backOffsetY);

            _descSizeTween = RectTransform
                .DOSizeDelta(_originalDescSize, _descDuration)
                .SetEase(_descEase)
                .OnUpdate(() =>
                {
                    RectTransform.anchoredPosition = backPos;

                    if (_layoutGroup)
                    {
                        LayoutRebuilder.MarkLayoutForRebuild(
                            _layoutGroup.transform as RectTransform
                        );
                    }
                })
                .OnComplete(() => { Canvas.ForceUpdateCanvases(); });
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
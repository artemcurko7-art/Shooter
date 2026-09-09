using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Image _background;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _checkMark;
        [SerializeField] private Sprite _redBackgroundSprite;
        [SerializeField] private Sprite _greenBackgroudSprite;
        [SerializeField] private Sprite _redFrameSprite;
        [SerializeField] private Sprite _greenFrameSprite;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TextAppear _textAppear;
        [SerializeField] private Button _button;
        [SerializeField] private float _descExpandHeight = 60f;
        [SerializeField] private float _descDuration = 0.3f;
        [SerializeField] private Ease _descEase = Ease.OutQuad;
        [SerializeField] private float _descSleepDelay = 3f;
        [SerializeField] private float _scrollDuration = 0.5f;

        private bool _isOpened;
        private bool _isDescExpanded;
        private ScrollRect _scrollRect;
        private Vector2 _originalDescSize;

        private Tween _descSizeTween;
        private Tween _descSleepTween;

        private VerticalLayoutGroup _layoutGroup;
        private RectTransform _rect;
        private Coroutine _scrollCoroutine;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();

            if (_rect)
                _originalDescSize = _rect.sizeDelta;

            if (_scrollRect && _scrollRect.content)
                _layoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();

            if (!_button)
                _button = GetComponent<Button>();

            if (_button)
                _button.onClick.AddListener(OnBarClicked);
        }

        private void OnEnable()
        {
            _textAppear.Enable();
        }

        private void OnDisable()
        {
            _textAppear.Disable();
        }

        private void OnDestroy()
        {
            _descSizeTween?.Kill();
            _descSleepTween?.Kill();

            if (_scrollCoroutine != null)
                StopCoroutine(_scrollCoroutine);

            if (_button != null)
                _button.onClick.RemoveListener(OnBarClicked);
        }

        public void Init(AchievementData.Achieve achieve, ScrollRect scrollRect)
        {
            _scrollRect = scrollRect;

            _name.text = achieve.GetLocalizedName(YG2.lang);
            _description.text = achieve.GetLocalizedDescription(YG2.lang);

            _checkMark.gameObject.SetActive(_isOpened);
            _lock.gameObject.SetActive(!_isOpened);

            _background.sprite = _isOpened ? _greenBackgroudSprite : _redBackgroundSprite;
            _frame.sprite = _isOpened ? _greenFrameSprite : _redFrameSprite;
        }

        private void OnBarClicked()
        {
            ToggleExpand();
        }

        private void ToggleExpand()
        {
            _descSizeTween?.Kill();
            _descSleepTween?.Kill();

            _isDescExpanded = !_isDescExpanded;

            var pivot = _rect.pivot;
            var targetSize = _isDescExpanded
                ? new Vector2(_originalDescSize.x, _originalDescSize.y + _descExpandHeight)
                : _originalDescSize;

            var currentSize = _rect.sizeDelta;
            var delta = targetSize - currentSize;

            var offsetY = -delta.y * (1f - pivot.y);
            var newPos = _rect.anchoredPosition + new Vector2(0, offsetY);

            _descSizeTween = _rect
                .DOSizeDelta(targetSize, _descDuration)
                .SetEase(_descEase)
                .OnUpdate(() =>
                {
                    _rect.anchoredPosition = newPos;

                    if (_layoutGroup)
                        LayoutRebuilder.MarkLayoutForRebuild(_layoutGroup.transform as RectTransform);
                });

            ScrollToTop();

            if (_isDescExpanded)
            {
                _descSleepTween = DOVirtual.DelayedCall(_descSleepDelay, () =>
                {
                    _descSizeTween?.Kill();

                    _isDescExpanded = false;

                    var curSize = _rect.sizeDelta;
                    var backDelta = _originalDescSize - curSize;
                    var backOffsetY = -backDelta.y * (1f - pivot.y);
                    var backPos = _rect.anchoredPosition + new Vector2(0, backOffsetY);

                    _descSizeTween = _rect
                        .DOSizeDelta(_originalDescSize, _descDuration)
                        .SetEase(_descEase)
                        .OnUpdate(() =>
                        {
                            _rect.anchoredPosition = backPos;

                            if (_layoutGroup)
                                LayoutRebuilder.MarkLayoutForRebuild(_layoutGroup.transform as RectTransform);
                        });
                });
            }
        }

        private void ScrollToTop()
        {
            if (!_scrollRect || !_rect) return;

            EnsureLayout();

            var contentRect = _scrollRect.content;
            var viewportRect = _scrollRect.viewport;

            var corners = new Vector3[4];
            _rect.GetWorldCorners(corners);

            var elementTop = corners[1];

            var viewportCorners = new Vector3[4];
            viewportRect.GetWorldCorners(viewportCorners);
            var viewportTop = viewportCorners[1];

            var deltaY = elementTop.y - viewportTop.y;

            if (Mathf.Abs(deltaY) < 1f)
                return;

            var targetY = contentRect.anchoredPosition.y + deltaY;
            targetY = GetClampedScrollY(contentRect, targetY);

            if (_scrollCoroutine != null)
                StopCoroutine(_scrollCoroutine);

            _scrollCoroutine = StartCoroutine(SmoothScroll(contentRect, targetY, _scrollDuration));
        }

        private void EnsureLayout()
        {
            Canvas.ForceUpdateCanvases();
            if (_layoutGroup != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup.transform as RectTransform);
            Canvas.ForceUpdateCanvases();
        }

        private IEnumerator SmoothScroll(RectTransform content, float targetY, float duration)
        {
            var start = content.anchoredPosition;

            if (duration <= 0f)
            {
                content.anchoredPosition = new Vector2(start.x, targetY);
                _scrollCoroutine = null;
                yield break;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var time = Mathf.Clamp01(elapsed / duration);
                time = Mathf.SmoothStep(0f, 1f, time);
                content.anchoredPosition = new Vector2(start.x, Mathf.Lerp(start.y, targetY, time));
                yield return null;
            }

            content.anchoredPosition = new Vector2(start.x, targetY);
            _scrollCoroutine = null;
        }

        private float GetClampedScrollY(RectTransform content, float targetY)
        {
            var contentHeight = content.rect.height;
            var viewportHeight = _scrollRect.viewport.rect.height;

            if (contentHeight <= viewportHeight)
                return content.anchoredPosition.y;

            const float maxY = 0f;
            var minY = -(contentHeight - viewportHeight);

            return Mathf.Clamp(targetY, minY, maxY);
        }
    }
}
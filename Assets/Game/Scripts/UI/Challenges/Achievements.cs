using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Challenges
{
    public class Achievements : Window
    {
        [SerializeField] private AchievementData _data;
        [SerializeField] private AchieveBar _barPrefab;
        [SerializeField] private RectTransform _content;
        [SerializeField] private TMP_Text _titleAchievesCount;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Скролл")]
        [SerializeField] private float _scrollDuration = 0.5f;
        [SerializeField] private float _topOffset = 0f;

        private readonly List<AchieveBar> _bars = new();

        private Coroutine _scrollCoroutine;

        private float _firstBarTopLocalY;
        private bool _firstBarPositionSaved;

        private void Start()
        {
            InitializeAchieves();
        }

        private void InitializeAchieves()
        {
            if (_titleAchievesCount)
                _titleAchievesCount.text = _data.Achieves.Count.ToString();

            _bars.Clear();

            foreach (var achieve in _data.Achieves)
            {
                var bar = Instantiate(_barPrefab, _content);

                bar.Init(
                    achieve,
                    _scrollRect,
                    ScrollToBar,
                    () => OnExpandComplete(bar)
                );

                _bars.Add(bar);
            }

            EnsureLayout();
            SaveFirstBarPosition();
        }

        private void EnsureLayout()
        {
            Canvas.ForceUpdateCanvases();

            if (_content)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_content);

            Canvas.ForceUpdateCanvases();
        }

        private float GetBarTopLocalY(RectTransform bar)
        {
            return bar.anchoredPosition.y +
                   bar.rect.height * (1f - bar.pivot.y);
        }

        private void SaveFirstBarPosition()
        {
            if (!_content || _content.childCount == 0)
                return;

            var firstBar = _content.GetChild(0) as RectTransform;

            if (!firstBar)
                return;

            _firstBarTopLocalY = GetBarTopLocalY(firstBar);
            _firstBarPositionSaved = true;

            Debug.Log($"FIRST BAR TOP SAVED: {_firstBarTopLocalY}");
        }

        private void ScrollToBar(RectTransform targetBar)
        {
            if (!targetBar)
                return;

            var bar = targetBar.GetComponent<AchieveBar>();

            if (!bar)
                return;

            if (!bar.IsDescExpanded)
            {
                bar.Expand();
                return;
            }

            ScrollAfterLayout(targetBar);
        }

        private void OnExpandComplete(AchieveBar bar)
        {
            if (!bar || !bar.RectTransform)
                return;

            EnsureLayout();

            ScrollAfterLayout(bar.RectTransform);
        }

        private void ScrollAfterLayout(RectTransform targetBar)
        {
            if (!_firstBarPositionSaved)
                return;

            if (!_scrollRect || !_scrollRect.content || !_scrollRect.viewport)
                return;

            if (!targetBar)
                return;

            EnsureLayout();

            var content = _scrollRect.content;

            var targetTopLocalY = GetBarTopLocalY(targetBar);
            var deltaY = targetTopLocalY - _firstBarTopLocalY;

            Debug.Log(
                $"CLICKED BAR: index={targetBar.GetSiblingIndex()}, " +
                $"targetTop={targetTopLocalY}, firstTop={_firstBarTopLocalY}"
            );

            Debug.Log(
                $"SCROLL: delta={deltaY}, " +
                $"contentBefore={content.anchoredPosition.y}"
            );

            var targetContentY =
                content.anchoredPosition.y - deltaY;

            targetContentY += _topOffset;

            var contentHeight = content.rect.height;
            var viewportHeight = _scrollRect.viewport.rect.height;

            var maxScroll = Mathf.Max(
                0f,
                contentHeight - viewportHeight
            );

            targetContentY = Mathf.Clamp(
                targetContentY,
                0f,
                maxScroll
            );

            Debug.Log(
                $"SCROLL: contentHeight={contentHeight}, " +
                $"viewportHeight={viewportHeight}, maxScroll={maxScroll}"
            );

            Debug.Log(
                $"SCROLL: targetContentY={targetContentY}"
            );

            if (_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }

            _scrollRect.StopMovement();

            if (Mathf.Abs(targetContentY - content.anchoredPosition.y) < 1f)
                return;

            _scrollCoroutine = StartCoroutine(SmoothScroll(content, targetContentY, _scrollDuration)
            );
        }

        private IEnumerator SmoothScroll(RectTransform content, float targetY, float duration)
        {
            var startY = content.anchoredPosition.y;

            if (duration <= 0f)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetY);

                _scrollCoroutine = null;
                yield break;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                t = Mathf.SmoothStep(0f, 1f,t);
                var currentY = Mathf.Lerp(startY, targetY, t);
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, currentY);

                yield return null;
            }

            content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetY);
            _scrollCoroutine = null;
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration
            );
        }

        protected override void Hide()
        {
            if (IsTransitionActive)
                return;

            if (_transition)
                _transition.Close(_canvasGroup, _rectTransform);
        }

        private void OnDisable()
        {
            if (_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }
        }
    }
}
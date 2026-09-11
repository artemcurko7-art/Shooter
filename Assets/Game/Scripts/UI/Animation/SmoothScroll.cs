using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class SmoothScroll : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;

        private Coroutine _scrollCoroutine;

        public bool IsScrolling => _scrollCoroutine != null;

        private void Awake()
        {
            if (!_scrollRect)
                _scrollRect = GetComponent<ScrollRect>();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void ScrollToY(float targetY, float duration, Action onComplete = null)
        {
            if (!_scrollRect || !_scrollRect.content)
            {
                onComplete?.Invoke();
                return;
            }

            Stop();

            _scrollCoroutine = StartCoroutine(
                SmoothScrollToY(targetY, duration, onComplete)
            );
        }

        public void ScrollToPosition(
            Vector2 targetPosition,
            float duration,
            Action onComplete = null)
        {
            if (!_scrollRect || !_scrollRect.content)
            {
                onComplete?.Invoke();
                return;
            }

            Stop();

            _scrollCoroutine = StartCoroutine(
                SmoothScrollToPosition(targetPosition, duration, onComplete)
            );
        }

        public void Stop()
        {
            if (_scrollCoroutine == null)
                return;

            StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = null;
        }

        private IEnumerator SmoothScrollToY(
            float targetY,
            float duration,
            Action onComplete)
        {
            var content = _scrollRect.content;
            var startY = content.anchoredPosition.y;

            if (duration <= 0f)
            {
                content.anchoredPosition = new Vector2(
                    content.anchoredPosition.x,
                    targetY
                );

                _scrollCoroutine = null;
                onComplete?.Invoke();
                yield break;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;

                var t = Mathf.Clamp01(elapsed / duration);
                t = Mathf.SmoothStep(0f, 1f, t);

                var currentY = Mathf.Lerp(startY, targetY, t);

                content.anchoredPosition = new Vector2(
                    content.anchoredPosition.x,
                    currentY
                );

                yield return null;
            }

            content.anchoredPosition = new Vector2(
                content.anchoredPosition.x,
                targetY
            );

            _scrollCoroutine = null;
            onComplete?.Invoke();
        }

        private IEnumerator SmoothScrollToPosition(
            Vector2 targetPosition,
            float duration,
            Action onComplete)
        {
            var content = _scrollRect.content;
            var startPosition = content.anchoredPosition;

            if (duration <= 0f)
            {
                content.anchoredPosition = targetPosition;

                _scrollCoroutine = null;
                onComplete?.Invoke();
                yield break;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;

                var t = Mathf.Clamp01(elapsed / duration);
                t = Mathf.SmoothStep(0f, 1f, t);

                content.anchoredPosition = Vector2.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

                yield return null;
            }

            content.anchoredPosition = targetPosition;

            _scrollCoroutine = null;
            onComplete?.Invoke();
        }
    }
}
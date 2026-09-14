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

        public void ScrollToY(
            float targetY,
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
                ScrollRoutine(
                    new Vector2(
                        _scrollRect.content.anchoredPosition.x,
                        targetY
                    ),
                    duration,
                    onComplete
                )
            );
        }

        public void ScrollToX(
            float targetX,
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
                ScrollRoutine(
                    new Vector2(
                        targetX,
                        _scrollRect.content.anchoredPosition.y
                    ),
                    duration,
                    onComplete
                )
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
                ScrollRoutine(
                    targetPosition,
                    duration,
                    onComplete
                )
            );
        }

        public void SetPosition(Vector2 position)
        {
            if (!_scrollRect || !_scrollRect.content)
                return;

            Stop();

            _scrollRect.content.anchoredPosition = position;
        }

        public void SetPositionY(float y)
        {
            if (!_scrollRect || !_scrollRect.content)
                return;

            Stop();

            var position = _scrollRect.content.anchoredPosition;
            position.y = y;

            _scrollRect.content.anchoredPosition = position;
        }

        public void SetPositionX(float x)
        {
            if (!_scrollRect || !_scrollRect.content)
                return;

            Stop();

            var position = _scrollRect.content.anchoredPosition;
            position.x = x;

            _scrollRect.content.anchoredPosition = position;
        }

        public void Stop()
        {
            if (_scrollCoroutine == null)
                return;

            StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = null;
        }

        private IEnumerator ScrollRoutine(
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
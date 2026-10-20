using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class SmoothScroll : UIAnimation
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease = Ease.OutExpo;

        private Tween _scrollTween;

        public ScrollRect GetScrollRect => _scrollRect;

        private void Awake()
        {
            if (!_scrollRect)
                _scrollRect = GetComponent<ScrollRect>();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void ScrollToY(float targetY, Action onComplete = null)
        {
            if (!TryGetContent(out var content))
            {
                onComplete?.Invoke();
                return;
            }

            var targetPosition = content.anchoredPosition;
            targetPosition.y = targetY;

            ScrollToPosition(targetPosition, onComplete);
        }

        public void ScrollToX(float targetX, Action onComplete = null)
        {
            if (!TryGetContent(out var content))
            {
                onComplete?.Invoke();
                return;
            }

            var targetPosition = content.anchoredPosition;
            targetPosition.x = targetX;

            ScrollToPosition(targetPosition, onComplete);
        }

        public void ScrollToPosition(Vector2 targetPosition, Action onComplete = null)
        {
            if (!TryGetContent(out var content))
            {
                onComplete?.Invoke();
                return;
            }

            Stop();

            if (_duration <= 0f)
            {
                content.anchoredPosition = targetPosition;
                onComplete?.Invoke();
                return;
            }

            _scrollTween = content.DOAnchorPos(targetPosition, _duration)
                .SetEase(_ease)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _scrollTween = null;
                    onComplete?.Invoke();
                });
        }

        public void SetPosition(Vector2 position)
        {
            if (!TryGetContent(out var content))
                return;

            Stop();
            content.anchoredPosition = position;
        }

        public void SetPositionY(float y)
        {
            if (!TryGetContent(out var content))
                return;

            var position = content.anchoredPosition;
            position.y = y;

            SetPosition(position);
        }

        public void SetPositionX(float x)
        {
            if (!TryGetContent(out var content))
                return;

            var position = content.anchoredPosition;
            position.x = x;

            SetPosition(position);
        }

        public void Stop()
        {
            if (_scrollTween == null)
                return;

            _scrollTween.Kill();
            _scrollTween = null;
        }

        private bool TryGetContent(out RectTransform content)
        {
            content = _scrollRect ? _scrollRect.content : null;
            return content;
        }
    }
}
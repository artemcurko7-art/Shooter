using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class AppearAnimation : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private float _scale = 1.15f;
        [SerializeField] private Ease _scaleEase = Ease.OutBack;

        private Sequence _sequence;
        private RectTransform _targetRect;
        private CanvasGroup _targetCanvasGroup;
        private Vector3 _initialScale;

        private void OnDisable()
        {
            KillAnimation();
        }

        private void OnDestroy()
        {
            KillAnimation();
        }

        public void Play(Image target, Action onHidden = null)
        {
            if (!target)
                return;

            Play(target.gameObject, onHidden);
        }

        public void Play(GameObject target, Action onHidden = null)
        {
            if (!target)
                return;

            KillAnimation();

            _targetRect = target.transform as RectTransform;

            if (!_targetRect)
                return;

            _targetCanvasGroup = target.GetComponent<CanvasGroup>();

            if (!_targetCanvasGroup)
                _targetCanvasGroup = target.AddComponent<CanvasGroup>();

            _initialScale = _targetRect.localScale;

            _targetCanvasGroup.alpha = 1f;
            _targetRect.localScale = _initialScale;

            _sequence = DOTween.Sequence();

            _sequence.Append(
                _targetCanvasGroup.DOFade(0f, _duration)
            );

            _sequence.AppendCallback(() =>
            {
                onHidden?.Invoke();
            });

            _sequence.Append(
                _targetCanvasGroup.DOFade(1f, _duration)
            );

            _sequence.Join(
                _targetRect
                    .DOScale(_initialScale * _scale, _duration)
                    .SetEase(_scaleEase)
            );

            _sequence.Append(
                _targetRect
                    .DOScale(_initialScale, _duration)
            );
        }

        public void SetVisible(Image target)
        {
            if (!target)
                return;

            SetVisible(target.gameObject);
        }

        public void SetVisible(GameObject target)
        {
            if (!target)
                return;

            var rect = target.transform as RectTransform;

            if (!rect)
                return;

            var canvasGroup = target.GetComponent<CanvasGroup>();

            if (!canvasGroup)
                canvasGroup = target.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 1f;
        }

        public void SetHidden(Image target)
        {
            if (!target)
                return;

            SetHidden(target.gameObject);
        }

        public void SetHidden(GameObject target)
        {
            if (!target)
                return;

            var canvasGroup = target.GetComponent<CanvasGroup>();

            if (!canvasGroup)
                canvasGroup = target.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
        }

        public void KillAnimation()
        {
            _sequence?.Kill();
            _sequence = null;

            if (_targetCanvasGroup)
                _targetCanvasGroup.DOKill();

            if (_targetRect)
                _targetRect.DOKill();

            _targetRect = null;
            _targetCanvasGroup = null;
        }
    }
}
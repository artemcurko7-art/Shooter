using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class AppearAnimation : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private float _scale = 1.15f;
        [SerializeField] private Ease _scaleEase = Ease.OutBack;

        private RectTransform _rectTransform;
        private Vector3 _initialScale;
        private Sequence _sequence;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;

            if (!_canvasGroup)
                _canvasGroup = GetComponent<CanvasGroup>();

            if (!_canvasGroup)
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            _initialScale = _rectTransform
                ? _rectTransform.localScale
                : transform.localScale;
        }

        private void OnDisable()
        {
            KillAnimation();
        }

        private void OnDestroy()
        {
            KillAnimation();
        }

        public void Play(Action onHidden = null)
        {
            if (!_canvasGroup)
                return;

            KillAnimation();

            SetVisibleState();

            _sequence = DOTween.Sequence();

            _sequence.Append(
                _canvasGroup.DOFade(0f, _duration)
            );

            _sequence.AppendCallback(() => { onHidden?.Invoke(); });

            _sequence.Append(
                _canvasGroup.DOFade(1f, _duration)
            );

            if (_rectTransform)
            {
                _sequence.Join(
                    _rectTransform
                        .DOScale(_initialScale * _scale, _duration)
                        .SetEase(_scaleEase)
                );

                _sequence.Append(
                    _rectTransform
                        .DOScale(_initialScale, _duration)
                );
            }
        }

        public void SetVisible()
        {
            KillAnimation();

            if (!_canvasGroup)
                return;

            _canvasGroup.alpha = 1f;

            if (_rectTransform)
                _rectTransform.localScale = _initialScale;
        }

        public void SetHidden()
        {
            KillAnimation();

            if (!_canvasGroup)
                return;

            _canvasGroup.alpha = 0f;

            if (_rectTransform)
                _rectTransform.localScale = _initialScale;
        }

        public void KillAnimation()
        {
            _sequence?.Kill();
            _sequence = null;

            if (_canvasGroup)
                _canvasGroup.DOKill();

            if (_rectTransform)
                _rectTransform.DOKill();
        }

        private void SetVisibleState()
        {
            _canvasGroup.alpha = 1f;

            if (_rectTransform)
                _rectTransform.localScale = _initialScale;
        }
    }
}
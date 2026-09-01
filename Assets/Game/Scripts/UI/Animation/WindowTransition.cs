using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class WindowTransition : MonoBehaviour
    {
        private const float CLOSE_DURATION = 0.25f;

        private readonly Vector3 _startScale = Vector3.one * 0.1f;
        private readonly Vector3 _endScale = Vector3.one;
        private readonly Vector3 _endPosition;

        [SerializeField] private Ease _fadeEase = Ease.Linear;

        private CanvasGroup _canvasGroup;
        private RectTransform _target;

        private void Awake()
        {
            if (_canvasGroup)
                _canvasGroup.alpha = 0f;
        }

        public void Init(CanvasGroup canvasGroup, RectTransform target)
        {
            _canvasGroup = canvasGroup;
            _target = target;
        }

        public void Open(Vector3 startPosition, Ease scaleEase, Ease positionEase, float duration)
        {
            if (!_canvasGroup)
                throw new ArgumentException("_canvasGroup не может быть null.", nameof(_canvasGroup));

            _target.position = startPosition;
            _target.localScale = _startScale;
            _canvasGroup.alpha = 0;

            if (_canvasGroup)
                _canvasGroup.interactable = false;

            _canvasGroup.DOFade(1f, duration / 2).SetEase(_fadeEase);
            _target.DOAnchorPos(_endPosition, duration).SetEase(positionEase);

            _target.DOScale(_endScale, duration).SetEase(scaleEase).OnComplete(() =>
            {
                if (_canvasGroup)
                    _canvasGroup.interactable = true;
            });
        }

        public void Close()
        {
            _target.DOScale(_startScale, CLOSE_DURATION).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (_canvasGroup)
                    _canvasGroup.interactable = false;

                _target.gameObject.SetActive(false);
            });
        }
    }
}
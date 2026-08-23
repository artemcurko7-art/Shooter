using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class PreviewTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _previewRectTransform;
        [SerializeField] private Vector3 _targetPreviewPosition;
        [SerializeField] private Vector3 _targetPreviewScale;

        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _positionEase = Ease.OutBack;
        [SerializeField] private Ease _scaleEase = Ease.OutBounce;
        [SerializeField] private Ease _fadeEase = Ease.Linear;

        private float _closeDuration = 0.25f;
        private Vector3 _startPreviewScale = Vector3.one * 0.1f;

        private void Awake()
        {
            if (_canvasGroup != null)
                _canvasGroup.alpha = 0f;
        }

        public void Open(RectTransform startPreviewPosition)
        {
            if (_canvasGroup == null)
                throw new ArgumentException("_canvasGroup не может быть null.", nameof(_canvasGroup));

            _targetPreviewScale = Vector3.one;

            _previewRectTransform.position = startPreviewPosition.position;
            _previewRectTransform.localScale = _startPreviewScale;
            _canvasGroup.alpha = 0;

            if (_canvasGroup != null)
                _canvasGroup.interactable = false;

            _canvasGroup.DOFade(1f, _duration / 2).SetEase(_fadeEase);

            _previewRectTransform.DOAnchorPos(_targetPreviewPosition, _duration).SetEase(_positionEase);

            _previewRectTransform.DOScale(_targetPreviewScale, _duration).SetEase(_scaleEase).OnComplete(() =>
            {
                if (_canvasGroup != null)
                    _canvasGroup.interactable = true;
            });
        }

        public void Close()
        {
            _targetPreviewScale = Vector3.zero;

            _previewRectTransform.DOScale(_targetPreviewScale, _closeDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (_canvasGroup != null)
                    _canvasGroup.interactable = false;

                gameObject.SetActive(false);
            });
        }
    }
}
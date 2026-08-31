using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class PreviewTransition : MonoBehaviour
    {
        private const float DURATION = 0.5f;
        private const float CLOSE_DURATION = 0.25f;
        
        private readonly Vector3 _startPreviewScale = Vector3.one * 0.1f;
        private readonly Vector3 _targetPreviewScale = Vector3.one;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _previewRectTransform;
        [SerializeField] private Vector3 _targetPreviewPosition;
        [SerializeField] private Ease _positionEase = Ease.OutBack;
        [SerializeField] private Ease _scaleEase = Ease.OutBounce;
        [SerializeField] private Ease _fadeEase = Ease.Linear;

        private void Awake()
        {
            if (_canvasGroup)
                _canvasGroup.alpha = 0f;
        }

        public void Open(RectTransform startPreviewPosition)
        {
            if (!_canvasGroup)
                throw new ArgumentException("_canvasGroup не может быть null.", nameof(_canvasGroup));

            _previewRectTransform.position = startPreviewPosition.position;
            _previewRectTransform.localScale = _startPreviewScale;
            _canvasGroup.alpha = 0;

            if (_canvasGroup)
                _canvasGroup.interactable = false;

            _canvasGroup.DOFade(1f, DURATION / 2).SetEase(_fadeEase);
            _previewRectTransform.DOAnchorPos(_targetPreviewPosition, DURATION).SetEase(_positionEase);
            
            _previewRectTransform.DOScale(_targetPreviewScale, DURATION).SetEase(_scaleEase).OnComplete(() =>
            {
                if (_canvasGroup)
                    _canvasGroup.interactable = true;
            });
        }

        public void Close()
        {
            _previewRectTransform.DOScale(_startPreviewScale, CLOSE_DURATION).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (_canvasGroup)
                    _canvasGroup.interactable = false;

                gameObject.SetActive(false);
            });
        }
    }
}
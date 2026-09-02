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
        private readonly Vector2 _endPosition = Vector2.zero;

        private Vector2 _originPosition = Vector2.zero;

        public void Open(CanvasGroup canvasGroup, RectTransform target, Vector3 startPosition, Ease scaleEase,
            Ease positionEase, float duration)
        {
            if (!canvasGroup)
                throw new ArgumentException("_canvasGroup не может быть null.", nameof(canvasGroup));

            if (!target)
                throw new ArgumentException("_target не может быть null.", nameof(target));

            target.gameObject.SetActive(true);
            target.position = startPosition;
            target.localScale = _startScale;
            canvasGroup.alpha = 0;

            _originPosition = target.anchoredPosition;

            if (canvasGroup)
                canvasGroup.interactable = false;

            canvasGroup.DOFade(1f, duration).SetEase(Ease.Linear);
            target.DOAnchorPos(_endPosition, duration).SetEase(positionEase);
            target.DOScale(_endScale, duration).SetEase(scaleEase).OnComplete(() =>
            {
                if (canvasGroup)
                    canvasGroup.interactable = true;
            });
        }

        public void Close(CanvasGroup canvasGroup, RectTransform target)
        {
            if (!canvasGroup)
                throw new ArgumentException("_canvasGroup не может быть null.", nameof(canvasGroup));

            if (!target)
                throw new ArgumentException("_target не может быть null.", nameof(target));

            canvasGroup.DOFade(0f, CLOSE_DURATION).SetEase(Ease.OutExpo);

            if (_originPosition != Vector2.zero) 
                target.DOAnchorPos(_originPosition, CLOSE_DURATION).SetEase(Ease.Linear);
            
            target.DOScale(_startScale, CLOSE_DURATION).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (canvasGroup)
                    canvasGroup.interactable = false;

                _originPosition = Vector2.zero;
                target.gameObject.SetActive(false);
            });
        }
    }
}
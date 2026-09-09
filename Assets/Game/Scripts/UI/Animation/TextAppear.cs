using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    [RequireComponent(typeof(TMP_Text), typeof(RectTransform))]
    public class TextAppear : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.35f;
        [SerializeField] private float _startScale = 0.85f;
        [SerializeField] private float _offsetY = 100f;

        private TMP_Text _text;
        private RectTransform _rectTransform;
        private Vector2 _initialPosition;
        private Vector3 _initialScale;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _rectTransform = GetComponent<RectTransform>();

            _initialPosition = _rectTransform.anchoredPosition;
            _initialScale = transform.localScale;
        }

        public void Enable()
        {
            Disable();

            var color = _text.color;
            color.a = 0f;
            _text.color = color;

            _rectTransform.anchoredPosition = _initialPosition + Vector2.up * _offsetY;
            transform.localScale = _initialScale * _startScale;

            var sequence = DOTween.Sequence();

            sequence.Join(
                _text.DOFade(1f, _duration)
                    .SetEase(Ease.OutSine)
            );

            sequence.Join(
                _rectTransform.DOAnchorPos(_initialPosition, _duration)
                    .SetEase(Ease.OutBack)
            );

            sequence.Join(
                transform.DOScale(_initialScale, _duration)
                    .SetEase(Ease.OutBack)
            );
        }

        public void Disable()
        {
            _text.DOKill();
            _rectTransform.DOKill();
            transform.DOKill();
        }
    }
}
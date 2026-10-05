using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class BuyEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;

        [Header("Animation")]
        [SerializeField] private Vector3 _endPosition = Vector3.zero;
        [SerializeField] private float _upOffset = 750f;
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private float _offsetDuration = 1f;
        [SerializeField] private float _fadeDuration = 2f;

        [Header("Ease")]
        [SerializeField] private Ease _scaleEase = Ease.OutBack;
        [SerializeField] private Ease _offsetEase = Ease.InOutQuad;
        [SerializeField] private Ease _fadeEase = Ease.InExpo;

        private readonly Vector3 _startScale = Vector3.one * 0.1f;
        private readonly Vector3 _endScale = Vector3.one;

        public bool IsAnimating { get; private set; }

        private void Awake()
        {
            if (_target == null)
                _target = GetComponent<RectTransform>();

            if (_target == null)
                Debug.LogWarning($"{nameof(BuyEffect)}: Target is not assigned.", this);

            if (_count == null)
                Debug.LogWarning($"{nameof(BuyEffect)}: Count is not assigned.", this);

            if (_canvasGroup == null)
                Debug.LogWarning($"{nameof(BuyEffect)}: CanvasGroup is not assigned.", this);

            if (_image == null)
                Debug.LogWarning($"{nameof(BuyEffect)}: Image is not assigned.", this);
        }

        private void OnDestroy()
        {
            KillAnimation();
        }

        public void Animate(Sprite icon, int count, Vector3 startPosition, Action onComplete = null)
        {
            if (!CanAnimate(icon))
                return;

            KillAnimation();

            gameObject.SetActive(true);

            _image.sprite = icon;
            _count.text = $"+{count}";
            _canvasGroup.alpha = 1f;

            _target.position = startPosition;
            _target.localScale = _startScale;

            IsAnimating = true;

            var endPosition = Vector3.zero;

            if (_endPosition != Vector3.zero)
                endPosition = _endPosition;
            else
                endPosition.y = startPosition.y + _upOffset;

            _target
                .DOLocalMove(endPosition, _offsetDuration)
                .SetEase(_offsetEase)
                .OnComplete(() => FadeOut(onComplete));

            _target
                .DOScale(_endScale, _scaleDuration)
                .SetEase(_scaleEase);
        }

        public void Stop()
        {
            if (!IsAnimating)
                return;

            KillAnimation();
            gameObject.SetActive(false);
            IsAnimating = false;
        }

        private void FadeOut(Action onComplete)
        {
            _canvasGroup
                .DOFade(0f, _fadeDuration)
                .SetEase(_fadeEase)
                .OnComplete(() =>
                {
                    IsAnimating = false;
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        private bool CanAnimate(Sprite icon)
        {
            if (_target == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Target is not assigned.", this);
                return false;
            }

            if (_canvasGroup == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: CanvasGroup is not assigned.", this);
                return false;
            }

            if (_image == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Image is not assigned.", this);
                return false;
            }

            if (_count == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Count is not assigned.", this);
                return false;
            }

            if (icon == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Icon is null.", this);
                return false;
            }

            return true;
        }

        private void KillAnimation()
        {
            if (_target != null)
                _target.DOKill();

            if (_canvasGroup != null)
                _canvasGroup.DOKill();

            IsAnimating = false;
        }
    }
}
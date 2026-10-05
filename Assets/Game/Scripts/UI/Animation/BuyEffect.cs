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
        [SerializeField] private float _upOffset = 750f;
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private float _offsetDuration = 1f;
        [SerializeField] private float _fadeDuration = 2f;
        [SerializeField] private Ease _scaleEase = Ease.OutBack;
        [SerializeField] private Ease _offsetEase = Ease.InOutQuad;
        [SerializeField] private Ease _fadeEase = Ease.InExpo;

        private readonly Vector3 _startScale = Vector3.one * 0.1f;
        private readonly Vector3 _endScale = Vector3.one;
        private readonly Vector2 _endPosition = Vector2.zero;

        public bool IsAnimating { get; private set; }

        private void Awake()
        {
            _target = GetComponent<RectTransform>();

            if (_target == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Target is not assigned.", this);
                return;
            }

            if (_image == null)
                Debug.LogWarning($"{nameof(BuyEffect)}: Image is not found on Target.", this);
        }

        private void OnDestroy()
        {
            if (_target != null)
                _target.DOKill();
        }

        public void Animate(Sprite icon, int count, Vector2 startPosition)
        {
            if (_target == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Target is not assigned.", this);
                return;
            }

            if (_image == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Image is not assigned.", this);
                return;
            }

            if (icon == null)
            {
                Debug.LogWarning($"{nameof(BuyEffect)}: Icon is null.", this);
                return;
            }

            _target.DOKill();
            _canvasGroup.alpha = 1f;
            _count.text = '+' + count.ToString();
            gameObject.SetActive(true);
            _image.sprite = icon;

            IsAnimating = true;

            var endPosition = startPosition;
            endPosition.y += _upOffset;

            _target.position = startPosition;
            _target.localScale = _startScale;

            _target
                .DOAnchorPos(endPosition, _offsetDuration)
                .SetEase(_offsetEase)
                .OnComplete(() =>
                {
                    _canvasGroup.DOFade(0, _fadeDuration)
                        .SetEase(_fadeEase)
                        .OnComplete(() =>
                        {
                            gameObject.SetActive(false);
                            IsAnimating = false;
                        });
                });

            _target
                .DOScale(_endScale, _scaleDuration)
                .SetEase(_scaleEase);
        }
    }
}
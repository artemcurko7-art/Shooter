using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    [RequireComponent(typeof(Button))]
    public class IconScaler : MonoBehaviour
    {
        [SerializeField] private string _name;

        [Header("Анимация")]
        [SerializeField] private bool _doUpOffset;
        [SerializeField] private float _offset;
        [SerializeField] private float _selectedScale = 1.2f;
        [SerializeField] private float _duration = 0.25f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private Button _button;

        private Vector3 _initialPosition;
        private Vector3 _initialScale;

        private Tween _scaleTween;
        private Tween _moveTween;

        public bool IsSelected { get; private set; }

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialPosition = transform.localPosition;

            if (!_button)
                _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_button)
                _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            if (_button)
                _button.onClick.RemoveListener(OnClicked);
        }

        public void SetSelected(bool selected, bool animated = true)
        {
            IsSelected = selected;

            var targetScale = selected
                ? _initialScale * _selectedScale
                : _initialScale;

            var targetPosition = selected
                ? _initialPosition + Vector3.up * _offset
                : _initialPosition;

            _scaleTween?.Kill();

            if (!animated)
            {
                transform.localScale = targetScale;
                transform.localPosition = targetPosition;
                return;
            }

            _scaleTween = transform
                .DOScale(targetScale, _duration)
                .SetEase(_ease)
                .OnComplete(() => _scaleTween = null);

            if (!_doUpOffset) return;
            _moveTween = transform
                .DOLocalMove(targetPosition, _duration)
                .SetEase(_ease)
                .OnComplete(() => _moveTween = null);
        }

        private void OnClicked()
        {
            var group = GetComponentInParent<IconScalerGroup>();

            if (group)
                group.Select(this, _name);
        }
    }
}
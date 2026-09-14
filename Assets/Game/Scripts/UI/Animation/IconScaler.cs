using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    [RequireComponent(typeof(Button))]
    public class IconScaler : MonoBehaviour
    {
        [Header("Анимация")]
        [SerializeField] private float _selectedScale = 1.2f;
        [SerializeField] private float _duration = 0.25f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private Button _button;

        private Vector3 _initialScale;
        private Tween _scaleTween;

        public bool IsSelected { get; private set; }

        private void Awake()
        {
            _initialScale = transform.localScale;

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

            _scaleTween?.Kill();
        }

        public void SetSelected(bool selected, bool animated = true)
        {
            IsSelected = selected;

            var targetScale = selected
                ? _initialScale * _selectedScale
                : _initialScale;

            _scaleTween?.Kill();

            if (!animated)
            {
                transform.localScale = targetScale;
                return;
            }

            _scaleTween = transform
                .DOScale(targetScale, _duration)
                .SetEase(_ease)
                .OnComplete(() => _scaleTween = null);
        }

        private void OnClicked()
        {
            IconScalerGroup group = GetComponentInParent<IconScalerGroup>();

            if (group)
                group.Select(this);
        }
    }
}
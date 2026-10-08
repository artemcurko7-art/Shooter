using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class SliderAnimotor : MonoBehaviour
    {
        [SerializeField] private bool _animateOnEnable = true;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private float _targetValue;
        private Tween _valueTween;

        private bool _initialized;

        private void OnEnable()
        {
            if (!_initialized)
                return;

            if (!_animateOnEnable)
                return;

            PlayAnimation();
        }

        private void OnDisable()
        {
            _valueTween?.Kill();
            _valueTween = null;
        }

        public void InitTarget(int targetValue)
        {
            _targetValue = targetValue;
            _initialized = true;

            if (!_animateOnEnable)
            {
                _slider.value = _targetValue;
            }
        }

        public void Play()
        {
            if (!_initialized)
                return;

            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _valueTween?.Kill();

            _slider.value = 0;

            _valueTween = DOTween
                .To(
                    () => 0f,
                    value => _slider.value = value,
                    _targetValue,
                    _duration)
                .SetEase(_ease)
                .SetUpdate(true)
                .SetDelay(0.5f);
        }
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class SliderDisplayer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private float _targetValue;
        private Tween _valueTween;

        private bool _initialized;
        private bool _hasBeenEnabled;

        private void OnEnable()
        {
            _hasBeenEnabled = true;

            if (!_initialized)
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

            if (_hasBeenEnabled && gameObject.activeInHierarchy)
                PlayAnimation();
        }

        private void PlayAnimation()
        {
            _valueTween?.Kill();

            _slider.value = 0;

            _valueTween = DOTween
                .To(() => 0f, value => _slider.value = value, _targetValue, _duration)
                .SetEase(_ease)
                .SetUpdate(true)
                .SetDelay(0.5f);
        }
    }
}
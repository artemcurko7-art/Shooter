using System;
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

        private void OnEnable()
        {
            if (_targetValue > 0)
                Animate(_targetValue);
        }
        
        public void InitTarget(int targetValue)
        {
            _targetValue = targetValue;
        }

        private void Animate(float targetValue)
        {
            _valueTween?.Kill();
            _targetValue = targetValue;
            _slider.value = 0;

            _valueTween = DOTween
                .To(() => 0, x => _slider.value = x, _targetValue, _duration)
                .SetEase(_ease)
                .SetUpdate(true);
        }
    }
}
using System;
using DG.Tweening;
using Game.Scripts.PhysicalBody;
using UnityEngine;

namespace Game.Scripts.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect : PhysicalBody<Effect>
    {
        private ParticleSystem _particleSystem;
        private Tween _tween;
        private Vector3 _currentScale;
        
        public event Action<Effect> Released;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _currentScale = transform.localScale;
        }

        public void Initialize(float? duration = null)
        {
            _tween?.Kill();

            float baseDuration = duration ?? _particleSystem.main.duration;

            _particleSystem.Play();

            float fadeDuration = 0.5f;
            float playTime = Mathf.Max(0f, baseDuration - fadeDuration);

            transform.localScale = _currentScale;
            
            var sequence = DOTween.Sequence();
  
            sequence.AppendInterval(playTime);
            sequence.Append(transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.InBack));
            
            sequence.OnComplete(() => Released?.Invoke(this));
            
            _tween = sequence;
        }
    }
}
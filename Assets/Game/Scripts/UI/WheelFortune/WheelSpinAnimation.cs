using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.WheelFortune
{
    public class WheelSpinAnimation : MonoBehaviour
    {
        private Tween _tween;

        public bool IsPlaying => _tween != null && _tween.IsActive() && _tween.IsPlaying();

        public void Play(
            float startScroll,
            float targetScroll,
            float duration,
            Ease ease,
            System.Action<float> onUpdate,
            System.Action onComplete)
        {
            Kill();

            _tween = DOTween.To(
                    () => startScroll,
                    value =>
                    {
                        startScroll = value;
                        onUpdate?.Invoke(value);
                    },
                    targetScroll,
                    duration
                )
                .SetEase(ease)
                .OnComplete(() =>
                {
                    _tween = null;
                    onComplete?.Invoke();
                });
        }

        public void Kill()
        {
            if (_tween == null)
                return;

            _tween.Kill();
            _tween = null;
        }

        private void OnDisable()
        {
            Kill();
        }
    }
}
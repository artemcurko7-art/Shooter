using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class ImageSlicer : MonoBehaviour
    {
        [SerializeField] private Image _target;
        [SerializeField] private float _duration;
        [SerializeField] private float _startPixelsMultiplier = 0.880f;
        [SerializeField] private float _endPixelsMultiplier = 0.960f;
        [SerializeField] private Ease _ease = Ease.OutQuart;

        private Tween _tween;

        private void Awake()
        {
            _target.pixelsPerUnitMultiplier = _startPixelsMultiplier;
        }

        private void OnEnable()
        {
            Animate();
        }

        private void OnDisable()
        {
            if (_tween != null && _tween.IsActive())
                _tween.Kill();

            if (_target)
                _target.pixelsPerUnitMultiplier = _startPixelsMultiplier;
        }

        public void Animate()
        {
            if (_tween != null && _tween.IsActive())
                _tween.Kill();

            _target.pixelsPerUnitMultiplier = _startPixelsMultiplier;

            _tween = DOTween.To(
                () => _target.pixelsPerUnitMultiplier,
                x => _target.pixelsPerUnitMultiplier = x,
                _endPixelsMultiplier, _duration
            ).SetEase(_ease);
        }
    }
}
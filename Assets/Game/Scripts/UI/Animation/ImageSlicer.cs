using System;
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

        private void OnEnable()
        {
            Animate();
        }

        private void Awake()
        {
            _target.pixelsPerUnitMultiplier = _startPixelsMultiplier;
        }

        public void Animate()
        {
            _target.pixelsPerUnitMultiplier = _startPixelsMultiplier;
            
            DOTween.To(() => _target.pixelsPerUnitMultiplier, x => _target.pixelsPerUnitMultiplier = x,
                _endPixelsMultiplier, _duration).SetEase(Ease.OutQuart);
        }
    }
}
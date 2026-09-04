using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class ImageMover : MonoBehaviour
    {
        [SerializeField] private List<Image> _images = new();
        [SerializeField] private float _amplitude = 15f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _stagger = 0.05f;

        private bool _isRunning;

        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void Enable()
        {
            if (_isRunning) return;
            _isRunning = true;

            for (var i = 0; i < _images.Count; i++)
            {
                var image = _images[i];
                if (!image) continue;

                image.transform.DOKill();
                image.transform.localPosition = Vector3.zero;

                var startY = image.transform.localPosition.y;
                var delay = i * _stagger;

                image.transform.DOLocalMoveY(startY + _amplitude, _duration)
                    .SetEase(Ease.InOutSine)
                    .SetDelay(delay)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }

        public void Disable()
        {
            _isRunning = false;

            foreach (var image in _images.Where(i => i))
            {
                image.transform.DOKill();
                image.transform.localPosition = Vector3.zero;
            }
        }
    }
}
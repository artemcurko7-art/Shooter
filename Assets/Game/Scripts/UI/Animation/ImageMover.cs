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
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private float _stagger = 0.05f;

        private Vector3 _zeroScale = new(1, 0, 1);
        private bool _isRunning;

        private void OnDisable()
        {
            Disable();
        }

        public void Enable()
        {
            if (_isRunning) return;
            _isRunning = true;

            transform.DOScale(Vector3.one, _scaleDuration).SetEase(Ease.OutExpo);

            for (var i = 0; i < _images.Count; i++)
            {
                var image = _images[i];
                if (!image) continue;

                image.transform.DOKill();
                image.transform.localPosition = Vector3.zero;

                var startY = image.transform.localPosition.y;
                var delay = i * _stagger;

                image.transform.DOLocalMoveY(startY + _amplitude, _moveDuration)
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

            if (!gameObject.activeInHierarchy) return;

            transform.DOScale(_zeroScale, _scaleDuration)
                .SetEase(Ease.OutExpo);
        }
    }
}
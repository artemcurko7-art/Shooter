
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class ImageBlinker : UIAnimation
    {
        [SerializeField] private List<Image> _images = new();
        [SerializeField] private float _cycleDurationPerImage = 0.6f;
        [SerializeField] private float _stepDelay = 0.7f;
        [SerializeField] private float _cycleDelay = 0.3f;
        [SerializeField] private Color _originalColor = Color.green;
        [SerializeField] private Color _glowColor = Color.black;

        private Coroutine _coroutine;
        private bool _isRunning;

        private void OnEnable()
        {
            if (_isRunning)
                StartSequence();
        }

        private void OnDisable()
        {
            StopSequence();
        }

        public void Enable()
        {
            _isRunning = true;

            if (isActiveAndEnabled)
                StartSequence();
        }

        public void Disable()
        {
            StopSequence();
            ResetToBaseColor();
        }

        public void ResetToBaseColor()
        {
            foreach (var image in _images)
            {
                if (!image)
                    continue;

                image.DOKill();
                image.color = _originalColor;
            }
        }

        private void StartSequence()
        {
            if (_coroutine != null || !isActiveAndEnabled || !_isRunning)
                return;

            _coroutine = StartCoroutine(RunSequence());
        }

        private void StopSequence()
        {
            _isRunning = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            foreach (var image in _images)
            {
                if (image)
                    image.DOKill();
            }
        }

        private IEnumerator RunSequence()
        {
            var halfStep = _cycleDurationPerImage * 0.5f;

            while (_isRunning)
            {
                var hasImages = false;

                for (var i = _images.Count - 1; i >= 0; i--)
                {
                    if (!_isRunning)
                        break;

                    var image = _images[i];

                    if (!image)
                        continue;

                    hasImages = true;

                    var target = Color.Lerp(_originalColor, _glowColor, 0.8f);

                    image.DOKill();

                    image.DOColor(target, halfStep)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            if (!_isRunning || !image)
                                return;

                            image.DOColor(_originalColor, halfStep)
                                .SetEase(Ease.InOutQuad);
                        });

                    yield return new WaitForSeconds(_stepDelay);
                }

                if (!hasImages)
                    break;

                yield return new WaitForSeconds(_cycleDelay);
            }

            _coroutine = null;
        }
    }
}

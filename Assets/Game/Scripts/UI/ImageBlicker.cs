using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ImageBlicker : MonoBehaviour
    {
        [SerializeField] private List<Image> _images = new();

        [SerializeField] private float _cycleDurationPerImage = 0.6f;
        [SerializeField] private float _stepDelay = 0.7f;
        [SerializeField] private float _cycleDelay = 0.3f;
        [SerializeField] private Color _originalColor = Color.green;
        [SerializeField] private Color _glowColor = Color.black;

        private Coroutine _coroutine;
        private bool _isRunning = false;

        public void Enable()
        {
            if (_isRunning) return;
            _isRunning = true;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(RunContinuousSequence());
        }

        public void Disable()
        {
            _isRunning = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            for (int i = 0; i < _images.Count; i++)
            {
                if (_images[i] != null)
                    _images[i].color = _originalColor;
            }
        }

        public void ResetToBaseColor()
        {
            foreach (var img in _images)
            {
                if (img != null)
                    img.color = _originalColor;
            }
        }

        private IEnumerator RunContinuousSequence()
        {
            float halfStep = _cycleDurationPerImage * 0.5f;

            while (_isRunning && _images.Count > 0)
            {
                for (int i = _images.Count - 1; i >= 0; i--)
                {
                    if (!_isRunning) break;

                    var image = _images[i];
                    if (image == null) continue;

                    Color original = _originalColor;
                    Color target = Color.Lerp(original, _glowColor, 0.8f);

                    image.DOColor(target, halfStep)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            if (!_isRunning || image == null) return;
                            image.DOColor(original, halfStep).SetEase(Ease.InOutQuad);
                        });

                    yield return new WaitForSeconds(_stepDelay);
                }

                yield return new WaitForSeconds(_cycleDelay);
            }
        }
    }
}
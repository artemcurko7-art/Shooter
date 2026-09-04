using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
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
        private bool _isRunning;

        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            Disable();
        }

        public void Enable()
        {
            if (_isRunning) return;
            _isRunning = true;
            _coroutine = StartCoroutine(RunSequence());
        }

        public void Disable()
        {
            _isRunning = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            foreach (var image in _images.Where(i => i))
            {
                image.DOKill();
            }

            ResetToBaseColor();
        }

        public void ResetToBaseColor()
        {
            foreach (var image in _images.Where(image => image))
            {
                image.color = _originalColor;
            }
        }

        private IEnumerator RunSequence()
        {
            var halfStep = _cycleDurationPerImage * 0.5f;

            while (_isRunning && _images.Count > 0)
            {
                for (var i = _images.Count - 1; i >= 0; i--)
                {
                    if (!_isRunning) yield break;

                    var image = _images[i];
                    if (!image) continue;

                    var original = _originalColor;
                    var target = Color.Lerp(original, _glowColor, 0.8f);

                    image.DOKill();

                    image.DOColor(target, halfStep)
                        .SetEase(Ease.InOutQuad)
                        .OnComplete(() =>
                        {
                            if (!_isRunning || !image) return;
                            image.DOColor(original, halfStep).SetEase(Ease.InOutQuad);
                        });

                    yield return new WaitForSeconds(_stepDelay);
                }

                yield return new WaitForSeconds(_cycleDelay);
            }
        }
    }
}

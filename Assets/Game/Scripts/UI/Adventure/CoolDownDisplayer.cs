using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Adventure
{
    public class CoolDownDisplayer : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private float _duration;
        [SerializeField] private float _sleepDelay;
        [SerializeField] private Ease _ease;

        [Header("Зависимости")]
        [SerializeField] private EnergyHandler _energyHandler;

        private Tween _sizeTween;
        private Tween _posTween;
        private Tween _sleepTween;
        private Vector2 _scalerSize;
        private Vector2 _originalSize;
        private bool _isScaled;

        private void Start()
        {
            _originalSize = Vector2.one;
            _scalerSize = _panel.sizeDelta;

            _panel.sizeDelta = Vector2.zero;
        }

        private void OnEnable()
        {
            _energyHandler.EnergyDecreased += OnEnergyDecreased;
        }

        private void OnDisable()
        {
            _energyHandler.EnergyDecreased -= OnEnergyDecreased;
        }

        private void OnEnergyDecreased()
        {
            ScaleAlt();
        }

        private void ScaleAlt()
        {
            _sizeTween?.Kill();
            _posTween?.Kill();
            _sleepTween?.Kill();

            _isScaled = !_isScaled;

            var targetSize = _isScaled ? _scalerSize : _originalSize;
            var currentSize = _panel.sizeDelta;
            var currentPos = _panel.anchoredPosition;

            var delta = targetSize - currentSize;
            var newPos = currentPos + new Vector2(-delta.x * 0.5f, delta.y * 0.5f);

            _sizeTween = _panel
                .DOSizeDelta(targetSize, _duration)
                .SetEase(_ease);

            _posTween = _panel
                .DOAnchorPos(newPos, _duration)
                .SetEase(_ease);

            if (_isScaled)
            {
                _sleepTween = DOVirtual.DelayedCall(_sleepDelay, () =>
                {
                    _sizeTween?.Kill();
                    _posTween?.Kill();
                    _isScaled = false;

                    var curSize = _panel.sizeDelta;
                    var curPos = _panel.anchoredPosition;
                    var backDelta = _originalSize - curSize;
                    var backPos = curPos + new Vector2(-backDelta.x * 0.5f, backDelta.y * 0.5f);

                    _sizeTween = _panel
                        .DOSizeDelta(_originalSize, _duration)
                        .SetEase(_ease);

                    _posTween = _panel
                        .DOAnchorPos(backPos, _duration)
                        .SetEase(_ease);
                });
            }
        }
    }
}
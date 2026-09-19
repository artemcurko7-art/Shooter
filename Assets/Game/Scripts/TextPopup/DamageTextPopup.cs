using System;
using DG.Tweening;
using Game.Scripts.PhysicalBody;
using TMPro;
using UnityEngine;

namespace Game.Scripts.TextPopup
{
    public class DamageTextPopup : PhysicalBody<DamageTextPopup>
    {
        [Header("General")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _valueText;
        
        [Header("Normal")]
        [SerializeField] private Color _normalColor;
        [SerializeField] private float _normalFontSize;
        [SerializeField] private float _normalDuration;
        [SerializeField] private float _normalFlyHeight;
        
        [Header("Critical")]
        [SerializeField] private Color _criticalColor;
        [SerializeField] private float _criticalFontSize;
        [SerializeField] private float _criticalDuration;
        [SerializeField] private float _criticalFlyHeight;
        
        public event Action<DamageTextPopup> Released;

        private Sequence _sequence;
        
        public void Initialize(Camera camera, int value, bool isCritical)
        {
            _canvas.worldCamera = camera;
            _valueText.text = value.ToString();
            _valueText.fontSize = isCritical ? _criticalFontSize : _normalFontSize;
            _valueText.color = isCritical ? _criticalColor : _normalColor;

            _sequence = DOTween.Sequence();
            
            if (isCritical)
                PlayCriticalAnimation();
            else
                PlayNormalAnimation();
        }

        private void PlayNormalAnimation()
        {
            _sequence
                .Join(transform.DOMoveY(transform.position.y + _normalFlyHeight, _normalDuration)
                .SetEase(Ease.OutCubic));
            
            _sequence
                .Join(_valueText.DOFade(0f, _normalDuration * 0.4f)
                .SetDelay(_normalDuration * 0.6f));

            _sequence.OnComplete(() => Released?.Invoke(this));
        }

        private void PlayCriticalAnimation()
        {
            _sequence
                .Append(transform.DOScale(1.4f, 0.15f).SetEase(Ease.OutBack));
            
            _sequence
                .Append(transform.DOScale(1.0f, 0.1f));

            _sequence
                .Join(transform.DOPunchPosition(new Vector3(0.2f, 0.2f, 0f), 0.25f, 10, 0.5f));
            
            _sequence
                .Join(transform.DOMoveY(transform.position.y + _criticalFlyHeight, _criticalDuration)
                .SetEase(Ease.OutQuad));
            
            _sequence
                .Join(_valueText.DOFade(0f, _criticalDuration * 0.3f)
                .SetDelay(_criticalDuration * 0.7f));

            _sequence.OnComplete(() => Released?.Invoke(this));
        }
    }
}
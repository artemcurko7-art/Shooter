using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.MV.StatContext.HealthViewContext
{
    public class HealthText : HealthView
    {
        [SerializeField] private TMP_Text _valueText;
        
        private Tween _tween;
        private float _currentDisplayValue = -1f;
        
        protected override void OnValueChanged(int value)
        {
            _tween?.Kill();
            
            if (_currentDisplayValue < 0)
            {
                _currentDisplayValue = value;
                _valueText.text = value.ToString();
                return;
            }

            _tween = DOTween.To(
                () => _currentDisplayValue, 
                x => 
                {
                    _currentDisplayValue = x;
                    _valueText.text = Mathf.RoundToInt(x).ToString(); 
                }, 
                value,
                0.3f
            ).SetEase(Ease.OutQuad);
        }
    }
}
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.MV.GameProgressContext
{
    public class GameProgressText : GameProgressView
    {
        [SerializeField] private TMP_Text _valueText;
        
        private Tween _tween;
        private float _oldValue;
        
        protected override void OnValueChanged(int value)
        {
            _tween?.Kill();
            
            float oldPercent = _oldValue / Model.MaxValue * 100f;
            float newPercent = (float)value / Model.MaxValue * 100f;
            
            _tween = DOTween.To(
                    () => oldPercent, 
                    x => 
                    {
                        _valueText.text = $"{x:0}%"; 
                    }, 
                    newPercent,
                    0.3f
                ).SetEase(Ease.OutQuad)
                .OnComplete(() => _oldValue = value); 
        }
    }
}
using System;
using System.Collections;
using DG.Tweening;
using Game.Scripts.PhysicalBody;
using TMPro;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Game.Scripts.PassiveAbilityContext
{
    public class PassiveAbilityView : PhysicalBody<PassiveAbilityView>
    {
        [SerializeField] private TMP_Text _valueText;
        
        private Vector3 _startPosition;
        private Sequence _animationSequence;
        private float _jumpHeight;
        private float _duration;

        public event Action<PassiveAbilityView> Released;
        
        public void Initialize(Color color, int value, float jumpHeight, float duration)
        {
            _valueText.color = color;
            _valueText.text = $"+{value}";
            _jumpHeight = jumpHeight;
            _duration = duration;
            _startPosition = transform.position;

            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _animationSequence?.Kill();
            
            _animationSequence = DOTween.Sequence();

            _animationSequence.Append(
                transform.DOMoveY(_startPosition.y + _jumpHeight, _duration)
                    .SetEase(Ease.OutCubic)
            );

            _animationSequence.Join(
                transform.DOScale(1.2f, _duration * 0.3f)
                    .SetEase(Ease.OutBack)
                    .SetLoops(2, LoopType.Yoyo)
            );

            _animationSequence.Join(
                _valueText.DOFade(0f, _duration * 0.7f)
                    .SetDelay(_duration * 0.3f)
            );
            
            _animationSequence.OnComplete(() => Released?.Invoke(this));
        }
    }
}
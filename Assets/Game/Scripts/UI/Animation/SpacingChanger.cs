using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class SpacingChanger : UIAnimation
    {
        private const float INITIAL_SPACING = 0;
        private float _targetSpacing;

        [SerializeField] private HorizontalOrVerticalLayoutGroup _layoutGroup;
        [SerializeField] private bool _animateOnEnable = true;
        [SerializeField] private Ease _ease = Ease.OutBack;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _delay = 0.5f;

        private Tweener _tween;

        private void Awake()
        {
            _targetSpacing = _layoutGroup.spacing;
        }

        private void OnEnable()
        {
            if (!_animateOnEnable)
                return;

            PlayAnimation();
        }

        public void Play()
        {
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            if (_tween != null && _tween.IsActive())
                _tween.Kill();

            _layoutGroup.spacing = INITIAL_SPACING;

            _tween = DOTween.To(
                () => _layoutGroup.spacing,
                x => _layoutGroup.spacing = x,
                _targetSpacing, _duration
            ).SetEase(_ease)
                .SetDelay(_delay);
        }
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class RewardBar : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;

        private RewardData.Reward _reward;
        private Vector3 _iconInitialScale = Vector3.one;
        private bool _iconScaleCached;

        public RectTransform RectTransform =>
            transform as RectTransform;

        public RewardData.Reward Reward =>
            _reward;

        public void Init(RewardData.Reward reward)
        {
            _reward = reward;

            if (_icon)
                _icon.DOKill();

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (_reward == null)
                return;

            if (_background)
                _background.sprite = _reward.background;

            if (_icon)
                _icon.sprite = _reward.icon;
        }

        public void AnimateIcon(float scale, float duration)
        {
            if (!_icon)
                return;

            var iconTransform = _icon.transform;

            iconTransform.DOKill();

            if (!_iconScaleCached)
            {
                _iconInitialScale = iconTransform.localScale;

                if (_iconInitialScale.sqrMagnitude < 0.0001f)
                    _iconInitialScale = Vector3.one;

                _iconScaleCached = true;
            }

            iconTransform.localScale = _iconInitialScale;

            var targetScale = _iconInitialScale * scale;

            var sequence = DOTween.Sequence();

            sequence.Append(
                iconTransform
                    .DOScale(targetScale, duration)
                    .SetEase(Ease.OutBack)
            );

            sequence.Append(
                iconTransform
                    .DOScale(_iconInitialScale, duration)
                    .SetEase(Ease.InOutSine)
            );
        }

        public void KillAnimation()
        {
            if (!_icon)
                return;

            _icon.transform.DOKill();

            if (_iconScaleCached)
                _icon.transform.localScale = _iconInitialScale;
        }
    }
}
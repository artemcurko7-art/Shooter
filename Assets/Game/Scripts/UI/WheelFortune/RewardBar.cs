using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class RewardBar : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;

        private RewardData.Reward _reward;

        public RectTransform Rect { get; private set; }

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        public void Init(RewardData.Reward reward)
        {
            _reward = reward;

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _background.sprite = _reward.background;
            _icon.sprite = _reward.icon;
        }
    }
}
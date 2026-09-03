using Game.Scripts.UI.Animation;
using UnityEngine;

namespace Game.Scripts.UI.Missions
{
    public class Missions : Window
    {
        [Header("Зависимости")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private WindowTransition _transition;
        
        protected override void Show()
        {
            _transition.Open(_canvasGroup, _panel, _openButton.transform.position, _scaleEase, _positionEase, _duration);
        }

        protected override void Hide()
        {
            if (IsTransitionActive) return;

            if (_transition)
            {
                _transition.Close(_canvasGroup, _rectTransform);
            }
        }
    }
}
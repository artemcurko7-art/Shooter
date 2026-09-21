using System;
using UnityEngine;

namespace Game.Scripts.UI.Gifts
{
    public class GiftSystem : Window
    {
        private void Start()
        {
            InitializeGifts();
        }

        private void InitializeGifts()
        {
            
        }

        protected override void Show()
                {
                    _transition.Open(
                        _canvasGroup,
                        _rectTransform,
                        _openButton.transform.position,
                        _scaleEase,
                        _positionEase,
                        _duration
                    );
                }
        
                protected override void Hide()
                {
                    if (IsTransitionActive)
                        return;
        
                    if (_transition)
                        _transition.Close(_canvasGroup, _rectTransform);
                }
    }
}

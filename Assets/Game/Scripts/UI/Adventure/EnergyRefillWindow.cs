using System;
using UnityEngine;

namespace Game.Scripts.UI.Adventure
{
    public class EnergyRefillWindow : Window
    {
        [SerializeField] private EnergyHandler _energyHandler;

        protected override void OnEnable()
        {
            base.OnEnable();
            _energyHandler.IsOutOfEnergy += OnOutOfEnergy;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _energyHandler.IsOutOfEnergy -= OnOutOfEnergy;
        }

        private void OnOutOfEnergy()
        {
            Show();
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition
            );
        }

        protected override void Hide()
        {
            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Close(
                _canvasGroup,
                _rectTransform,
                FinishTransition
            );
        }
    }
}
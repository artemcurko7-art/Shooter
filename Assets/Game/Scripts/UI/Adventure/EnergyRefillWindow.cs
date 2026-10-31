using UnityEngine;

namespace Game.Scripts.UI.Adventure
{
    public class EnergyRefillWindow : Window
    {
        [SerializeField] private EnergyHandler _energyHandler;
        [SerializeField] private RectTransform _startAnimTransform;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_energyHandler != null)
                _energyHandler.IsOutOfEnergy += OnOutOfEnergy;
        }

        protected override void OnDisable()
        {
            if (_energyHandler != null)
                _energyHandler.IsOutOfEnergy -= OnOutOfEnergy;

            base.OnDisable();
        }

        private void OnOutOfEnergy()
        {
            if (_energyHandler == null || _energyHandler.CurrentEnergy > 0)
                return;

            if (!TryBeginOpen())
                return;

            Show();
        }

        protected override void Show()
        {
            if (!_transition || !_canvasGroup || !_rectTransform)
            {
                Debug.LogError(
                    "[EnergyRefillWindow] Не назначены ссылки: WindowTransition, CanvasGroup или RectTransform.",
                    this);

                FinishTransition();
                return;
            }

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _startAnimTransform.position,
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
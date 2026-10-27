
using System;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Adventure
{
    public class EnergyHandler : MonoBehaviour
    {
        [SerializeField] private int _maxEnergyCount = 5;
        [SerializeField] private RectTransform _popPosition;

        [Header("Зависимости")]
        [SerializeField] private PlayButtonHandler _buttonHandler;
        [SerializeField] private EnergyVisualizer _visualizer;
        [SerializeField] private SceneSwitcher _sceneSwitcher;
        [SerializeField] private EnergyRestoreService _restoreService;

        public event Action IsOutOfEnergy;
        public event Action EnergyDecreased;

        private void OnEnable()
        {
            if (_buttonHandler)
                _buttonHandler.Clicked += OnPlayButtonClicked;

            if (_restoreService)
                _restoreService.EnergyChanged += OnEnergyChanged;
        }

        private void OnDisable()
        {
            if (_buttonHandler)
                _buttonHandler.Clicked -= OnPlayButtonClicked;

            if (_restoreService)
                _restoreService.EnergyChanged -= OnEnergyChanged;
        }

        private void Awake()
        {
            YG2.saves.Energy = Mathf.Clamp(YG2.saves.Energy, 0, _maxEnergyCount);

            if (_visualizer)
                _visualizer.UpdateVisual(YG2.saves.Energy);
        }

        private void OnEnergyChanged(float energy)
        {
            if (_visualizer)
                _visualizer.UpdateVisual(Mathf.RoundToInt(energy));
        }

        private void OnPlayButtonClicked()
        {
            if (HasNoEnergy())
            {
                IsOutOfEnergy?.Invoke();
                return;
            }

            if (!IsValidToStart())
                return;

            DecreaseEnergy();

            if (_visualizer && _popPosition)
                _visualizer.DisplayPopEffect(_popPosition.position);

            // _sceneSwitcher.LoadSavedScene();
        }

        private void DecreaseEnergy()
        {
            YG2.saves.Energy--;
            YG2.SaveProgress();

            if (_visualizer)
                _visualizer.UpdateVisual(YG2.saves.Energy);

            _restoreService?.OnEnergySpent();
            EnergyDecreased?.Invoke();
        }

        private bool IsValidToStart()
        {
            return !HasNoEnergy() &&
                   (!_visualizer || !_visualizer.IsAnimating) &&
                   (!_sceneSwitcher || !_sceneSwitcher.IsEnabled);
        }

        private bool HasNoEnergy()
        {
            return YG2.saves.Energy <= 0;
        }
    }
}

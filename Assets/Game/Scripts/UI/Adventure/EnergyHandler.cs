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

        public event Action IsOutOfEnergy;
        public event Action EnergyDecreased;

        private void OnEnable()
        {
            _buttonHandler.Clicked += OnPlayButtonClicked;
        }

        private void OnDisable()
        {
            _buttonHandler.Clicked -= OnPlayButtonClicked;
        }

        private void Awake()
        {
            YG2.saves.Energy = _maxEnergyCount;


            var count = _maxEnergyCount >= YG2.saves.Energy ? YG2.saves.Energy : _maxEnergyCount;
            _visualizer.UpdateVisual(count);
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
            _visualizer.DisplayPopEffect(_popPosition.position);
            //_sceneSwitcher.LoadSavedScene();
        }

        private void DecreaseEnergy()
        {
            YG2.saves.Energy--;
            YG2.SaveProgress();
            _visualizer.UpdateVisual(YG2.saves.Energy);
            EnergyDecreased?.Invoke();
        }

        private bool IsValidToStart()
        {
            return !HasNoEnergy() && !_visualizer.IsAnimating && !_sceneSwitcher.IsEnabled;
        }

        private bool HasNoEnergy()
        {
            return YG2.saves.Energy == 0;
        }
    }
}
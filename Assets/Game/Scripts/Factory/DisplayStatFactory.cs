using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Replacement;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class DisplayStatFactory
    {
        private readonly DisplayStat _display;
        private readonly DiContainer _container;
        
        public DisplayStatFactory(DisplayStat display, DiContainer container)
        {
            _display = display;
            _container = container;
        }

        public DisplayStat Create(StatViewConfig viewConfig, Transform container, int value, bool IsPercentageValue)
        {
            var display = _container.InstantiatePrefabForComponent<DisplayStat>(_display, Vector3.zero, Quaternion.identity, container);
            display.Initialize(viewConfig.Type, viewConfig.Icon, viewConfig.Name, value, IsPercentageValue);
            display.transform.localScale = Vector3.one;
            
            return display;
        }
    }
}
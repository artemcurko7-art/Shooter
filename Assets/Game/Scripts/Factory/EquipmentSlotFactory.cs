using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.MV.StatContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class EquipmentSlotFactory
    {
        private readonly EquipmentSlot _equipmentSlot;
        private readonly DiContainer _container;
        
        public EquipmentSlotFactory(EquipmentSlot slot, DiContainer container)
        {
            _equipmentSlot = slot;
            _container = container;
        }

        public EquipmentSlot Create(RarityConfig rarityConfig, EquipmentConfig equipmentConfig, Stat mainStat, Stat[] additionalStats, Transform container)
        {
            mainStat.Increase(mainStat.Value * rarityConfig.Multiplier);
            
            foreach (var stat in additionalStats)
                stat.Increase(stat.Value * rarityConfig.Multiplier);
            
            var view = _container.InstantiatePrefabForComponent<EquipmentSlot>(_equipmentSlot, Vector3.zero, Quaternion.identity, container);
            var equipment = new EquipmentItem(mainStat, additionalStats, equipmentConfig.Type, equipmentConfig.WeaponType);
            view.Initialize(rarityConfig, equipmentConfig.Icon, equipmentConfig.Name);
            view.Initialize(equipment);
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}
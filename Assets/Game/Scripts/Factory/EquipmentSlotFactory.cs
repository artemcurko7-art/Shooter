using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.MV.StatContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class EquipmentSlotFactory
    {
        private readonly Slot _slot;
        private readonly DiContainer _container;
        
        public EquipmentSlotFactory(Slot slot, DiContainer container)
        {
            _slot = slot;
            _container = container;
        }

        public Slot Create(RarityEquipmentConfig rarityEquipmentConfig, EquipmentConfig equipmentConfig, Stat mainStat, Stat[] additionalStats, Transform container)
        {
            mainStat.Increase(mainStat.Value * rarityEquipmentConfig.Multiplier);
            
            foreach (var stat in additionalStats)
                stat.Increase(stat.Value * rarityEquipmentConfig.Multiplier);
            
            var view = _container.InstantiatePrefabForComponent<Slot>(_slot, Vector3.zero, Quaternion.identity, container);
            var equipment = new EquipmentItem(mainStat, additionalStats, equipmentConfig.Type, equipmentConfig.WeaponType);
            view.Initialize(rarityEquipmentConfig, equipment, equipmentConfig.Icon, equipmentConfig.Name);
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}
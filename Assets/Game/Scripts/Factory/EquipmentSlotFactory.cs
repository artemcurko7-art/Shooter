using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
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

        public EquipmentSlot Create(
            RarityConfig rarityConfig,
            EquipmentConfig equipmentConfig,
            Stat mainStat,
            IReadOnlyDictionary<StatType, Stat> additionalStats,
            Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<EquipmentSlot>(_equipmentSlot, Vector3.zero, Quaternion.identity, container);
            var equipment = new EquipmentItem(mainStat, additionalStats, equipmentConfig.Type, equipmentConfig.WeaponType);
            view.Initialize(rarityConfig, equipmentConfig.Icon, equipmentConfig.Name);
            view.Initialize(equipment);
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}
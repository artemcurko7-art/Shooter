using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.Handler;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Data;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.Service.Equipment
{
    public class EquipmentService : IEquipmentService
    {
        private readonly EquipmentData _data;
        private readonly RarityEquipmentData _rarityData;
        private readonly StatData _statData;
        private readonly SlotHandler _slotHandler;
        private readonly EquipmentSlotFactory _slotFactory;
        private readonly Transform _container;
        
        public event Action<Slot> Added;
        
        public EquipmentService(
            RarityEquipmentData rarityData, 
            EquipmentData data, 
            StatData statData,
            SlotHandler slotHandler,
            EquipmentSlotFactory slotFactory, 
            Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _statData = statData;
            _slotHandler = slotHandler;
            _slotFactory = slotFactory;
            _container = container;
        }
        
        public IReadOnlyList<Slot> Slots => _slotHandler.Slots;

        public void OnClick()
        {
            RarityEquipmentType rarityEquipmentType = GetRandomRarityEquipmentType();
            EquipmentType equipmentType = GetRandomEquipmentType();
            Stat[] stats = GetRandomStats(equipmentType).ToArray();
            EquipmentConfig config = GetRandomEquipmentConfig(equipmentType);

            var slot = _slotFactory.Create(_rarityData.Configs[rarityEquipmentType], config, stats, _container);
            _slotHandler.Add(slot);
            Added?.Invoke(slot);
            
            // Debug.Log($"Instance: Rarity: {slot.RarityEquipmentType}, Equipment: {slot.EquipmentItem.Type}");
            //
            // foreach (var stat in slot.EquipmentItem.Stats)
            // {
            //     Debug.Log($"Stat Type: {stat.Type}, Stat Value {stat.Value}, Percent: {stat.IsPercentageValue}");
            // }
        }
        // Rarity 2 5 10 15 25 43 == 100

        private EquipmentConfig GetRandomEquipmentConfig(EquipmentType equipmentType)
        {
            Random random = new();

            int index = random.Next(0, _data.Configs[equipmentType].Count);
            
            EquipmentConfig config = _data.Configs[equipmentType][index];
            
            return config;
        }
        
        private RarityEquipmentType GetRandomRarityEquipmentType()
        {
            RarityEquipmentType rarityEquipmentType = RarityEquipmentType.None;
            Random rarityEquipmentRandom = new Random();
            int rarityEquipmentTypeIndex = rarityEquipmentRandom.Next(1, Enum.GetValues(typeof(RarityEquipmentType)).Length);
            int rarityEquipmentTypeFoundIndex = 0;

            foreach (var type in _rarityData.Configs.Keys)
            {
                if (type == RarityEquipmentType.None)
                    continue;
                
                rarityEquipmentTypeFoundIndex++;
                
                if (rarityEquipmentTypeFoundIndex == rarityEquipmentTypeIndex)
                    rarityEquipmentType = type;
            }

            return rarityEquipmentType;
        } 
        
        private EquipmentType GetRandomEquipmentType()
        {
            EquipmentType equipmentType = EquipmentType.None;
            Random equipmentRandom = new Random();
            int equipmentTypeIndex = equipmentRandom.Next(1, Enum.GetValues(typeof(EquipmentType)).Length);
            int equipmentTypeFoundIndex = 0;

            foreach (var type in _data.Configs.Keys)
            {
                if (type == EquipmentType.None)
                    continue;
                
                equipmentTypeFoundIndex++;
                
                if (equipmentTypeFoundIndex == equipmentTypeIndex)
                    equipmentType = type;
            }
            
            return equipmentType;
        }

        private List<Stat> GetRandomStats(EquipmentType equipmentType)
        {
            List<StatInfoData> statInfoDates = new List<StatInfoData>(_statData.AdditionalStats[equipmentType].Stats);
            List<Stat> stats = new();
            Random random = new();
            
            int count = random.Next(0, statInfoDates.Count - 1);

            for (int j = 0; j < count; j++)
            {
                int index = random.Next(0, statInfoDates.Count);
                statInfoDates.RemoveAt(index);
            }

            foreach (var statInfoData in statInfoDates)
            {
                int value = random.Next((int)statInfoData.MinValue, (int)statInfoData.MaxValue);
                
                stats.Add(CreateStatInstance(statInfoData.Type, value, statInfoData.IsPercentageValue));
            }
            
            return stats;
        }
        
        private Stat CreateStatInstance(StatType type, int value, bool isPercentageValue) => type switch
        {
            StatType.Health => new Health(value, isPercentageValue),
            StatType.Attack => new Damage(value, isPercentageValue),
            StatType.Defence => new Defence(value, isPercentageValue),
            StatType.CriticalChance => new CriticalChance(value, isPercentageValue),
            StatType.CriticalDamage => new CriticalDamage(value, isPercentageValue),
            _ => null
        };
    }
}
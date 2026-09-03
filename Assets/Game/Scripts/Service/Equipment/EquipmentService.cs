using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Data;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UserUtils;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class EquipmentService : IEquipmentService
    {
        private readonly EquipmentData _data;
        private readonly RarityEquipmentData _rarityData;
        private readonly EquipmentSlotFactory _slotFactory;
        private readonly Transform _container;
        
        public event Action<Slot> Added;
        
        public EquipmentService(
            RarityEquipmentData rarityData, 
            EquipmentData data, 
            EquipmentSlotFactory slotFactory, 
            Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _slotFactory = slotFactory;
            _container = container;
        }

        public void OnClick()
        {
            RarityEquipmentType rarityEquipmentType = WeightedRandomSampling.GetRandomWeighted<RarityEquipmentType>();
            EquipmentType equipmentType = WeightedRandomSampling.GetRandomWeighted<EquipmentType>();
            EquipmentConfig config = GetRandomEquipmentConfig(equipmentType, out int index);          
            Stat stat = GetRandomStat(equipmentType, index);
            Stat[] stats = GetRandomStats(rarityEquipmentType, equipmentType, index);
            
            var slot = _slotFactory.Create(_rarityData.Configs[rarityEquipmentType], config, stat, stats, _container);
            Added?.Invoke(slot);
        }

        private Stat GetRandomStat(EquipmentType equipmentType, int index)
        {
            List<StatInfoData> statInfoDates = new List<StatInfoData>(_data.Configs[equipmentType][index].MainStats);
            
            int randomIndex = NumberGeneration.GetRandom(0, _data.Configs[equipmentType][index].MainStats.Length - 1);
            int value = NumberGeneration.GetRandom((int)statInfoDates[randomIndex].MinValue, (int)statInfoDates[randomIndex].MaxValue);
            var stat = CreateStatInstance(statInfoDates[randomIndex].Type, value, statInfoDates[randomIndex].IsPercentageValue);
            
            return stat;
        }
        
        private EquipmentConfig GetRandomEquipmentConfig(EquipmentType equipmentType, out int index)
        {
            index = NumberGeneration.GetRandom(0, _data.Configs[equipmentType].Count - 1);
            EquipmentConfig config = _data.Configs[equipmentType][index];
            
            return config;
        }

        private Stat[] GetRandomStats(RarityEquipmentType rarityEquipmentType, EquipmentType equipmentType, int index)
        {
            List<StatInfoData> statInfoDates = new List<StatInfoData>(_data.Configs[equipmentType][index].AdditionalStats);
            List<Stat> stats = new();
            statInfoDates.Shuffle();
            
            int count = NumberGeneration.GetRandom(_rarityData.Configs[rarityEquipmentType].MaxParameter - 1, _rarityData.Configs[rarityEquipmentType].MaxParameter + 1);
            int calculationCountParameter = statInfoDates.Count - count;

            for (int j = 0; j < calculationCountParameter; j++)
            {
                 int randomIndex = NumberGeneration.GetRandom(0, statInfoDates.Count - 1);
                 statInfoDates.RemoveAt(randomIndex);
            }

            foreach (var statInfoData in statInfoDates)
            {
                int value = NumberGeneration.GetRandom((int)statInfoData.MinValue, (int)statInfoData.MaxValue);
                
                stats.Add(CreateStatInstance(statInfoData.Type, value, statInfoData.IsPercentageValue));
            }
            
            return stats.ToArray();
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
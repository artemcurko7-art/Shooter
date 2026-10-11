using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Equipment.EquipmentContext.Data;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.UserUtils;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.EquipmentContext
{
    public class EquipmentSlotRewardService : SlotRewardService, ISlotRewardService<EquipmentSlot>
    {
        private readonly EquipmentData _data;
        private readonly EquipmentSlotFactory _factory;
        private int _countType;
        private bool _isPercent;
        
        public event Action<EquipmentSlot> Rewarded;
        
        public EquipmentSlotRewardService(
            RarityData rarityData,
            Transform container, EquipmentData data,
            EquipmentSlotFactory factory)
            : base(rarityData, container)
        {
            _data = data;
            _factory = factory;
        }
        
        public override void Execute()
        {
            RarityType rarityType = WeightedRandomSampling.GetRandomWeighted<RarityType>();
            EquipmentType equipmentType = WeightedRandomSampling.GetRandomWeighted<EquipmentType>();
            EquipmentConfig config = GetRandomEquipmentConfig(equipmentType, out int index);
            Stat mainStat = GetRandomStat(equipmentType, index);
            Stat[] additionalStatsRandom = GetRandomStats(rarityType, equipmentType, index);
            additionalStatsRandom = GetSortingStats(additionalStatsRandom.ToList());

            mainStat.Increase(mainStat.Value * RarityData.Configs[rarityType].Multiplier);
            
            Dictionary<StatType, Stat> stats = new();
            
            foreach (var stat in additionalStatsRandom)
            {
                stats.Add(stat.Type, stat);
                stats[stat.Type].Increase(stats[stat.Type].Value * RarityData.Configs[rarityType].Multiplier);
            }
            
            var slot = _factory.Create(RarityData.Configs[rarityType], config, mainStat, stats, Container);
            Rewarded?.Invoke(slot);
        }
        
        private Stat GetRandomStat(EquipmentType equipmentType, int index)
        {
            List<EquipmentStatData> statDates = new List<EquipmentStatData>(_data.Configs[equipmentType][index].MainStats);
            
            int randomIndex = NumberGeneration.GetIntegerRandom(0, _data.Configs[equipmentType][index].MainStats.Length - 1);
            int value = NumberGeneration.GetIntegerRandom((int)statDates[randomIndex].MinValue, (int)statDates[randomIndex].MaxValue);
            var stat = CreateStatInstance(statDates[randomIndex].Type, value, statDates[randomIndex].IsPercentageValue);
            
            return stat;
        }
        
        private EquipmentConfig GetRandomEquipmentConfig(EquipmentType equipmentType, out int index)
        {
            index = NumberGeneration.GetIntegerRandom(0, _data.Configs[equipmentType].Count - 1);
            EquipmentConfig config = _data.Configs[equipmentType][index];
            
            return config;
        }
        
        private Stat[] GetRandomStats(RarityType rarityType, EquipmentType equipmentType, int index)
        {
            List<EquipmentStatData> statInfoDates = new List<EquipmentStatData>(_data.Configs[equipmentType][index].AdditionalStats);
            List<Stat> stats = new();
            statInfoDates.Shuffle();
            
            int count = NumberGeneration.GetIntegerRandom(RarityData.Configs[rarityType].MaxParameter - 1, RarityData.Configs[rarityType].MaxParameter + 1);
            int calculationCountParameter = statInfoDates.Count - count;
        
            for (int j = 0; j < calculationCountParameter; j++)
            {
                 int randomIndex = NumberGeneration.GetIntegerRandom(0, statInfoDates.Count - 1);
                 statInfoDates.RemoveAt(randomIndex);
            }
        
            foreach (var statInfoData in statInfoDates)
            {
                int value = NumberGeneration.GetIntegerRandom((int)statInfoData.MinValue, (int)statInfoData.MaxValue);
                
                stats.Add(CreateStatInstance(statInfoData.Type, value, statInfoData.IsPercentageValue));
            }
            
            return stats.ToArray();
        }
        
        private Stat[] GetSortingStats(List<Stat> stats)
        {
            stats = stats
                .OrderBy(s => s.Type)
                .ThenByDescending(s => s.IsPercentageValue)
                .ToList();
            
            return stats.ToArray();
        }
        
        private Stat CreateStatInstance(StatType type, int value, bool isPercentageValue) => type switch
        {
            StatType.Health => new Health(value, isPercentageValue),
            StatType.Damage => new Damage(value, isPercentageValue),
            StatType.Defence => new Defence(value, isPercentageValue),
            StatType.CriticalChance => new CriticalChance(value, isPercentageValue),
            StatType.CriticalDamage => new CriticalDamage(value, isPercentageValue),
            _ => null
        };
    }
}
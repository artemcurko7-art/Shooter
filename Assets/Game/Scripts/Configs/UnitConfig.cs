using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Unit", fileName = "Unit", order = 3)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public UnitType Type { get; private set; }
        [field: SerializeField] public UnitAttackerType AttackerType { get; private set; }
        [field: SerializeField] public Unit Unit { get; private set; }
        [field: SerializeField] public UnitStatData[] Stats { get; private set; }
        
        private readonly List<StatType> _excludedStatTypes = new();
        
        private void Reset()
        {
            RebuildStatsArray();
        }

        private void OnValidate()
        {
            var requiredTypes = GetFilteredStatTypes();

            if (Stats == null || Stats.Length != requiredTypes.Length)
                RebuildStatsArray();
        }

        private void RebuildStatsArray()
        {
            var statTypes = GetFilteredStatTypes();
            var newStats = new UnitStatData[statTypes.Length];
            
            for (int i = 0; i < statTypes.Length; i++)
            {
                var currentType = statTypes[i];
                int existingValue = 0;
                
                if (Stats != null)
                {
                    var existingStat = Stats.FirstOrDefault(characterStat => characterStat != null && characterStat.Type == currentType);

                    if (existingStat != null)
                    {
                        existingValue = existingStat.Value;
                    }
                }
            
                newStats[i] = new UnitStatData(currentType, existingValue);
            }
            
            Stats = newStats;
        }

        private StatType[] GetFilteredStatTypes()
        {
            if (_excludedStatTypes.Contains(StatType.CriticalChance) == false)
                _excludedStatTypes.Add(StatType.CriticalChance);

            if (_excludedStatTypes.Contains(StatType.CriticalDamage) == false)
                _excludedStatTypes.Add(StatType.CriticalDamage);
            
            return Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .Where(statType => Convert.ToInt32(statType) != 0)
                .Except(_excludedStatTypes)
                .ToArray();
        }
    }
}
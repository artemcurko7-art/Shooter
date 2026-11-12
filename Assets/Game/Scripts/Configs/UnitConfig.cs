using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.ProjectileContext.Type;
using Game.Scripts.UnitContext;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Unit", fileName = "Unit", order = 3)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public UnitType Type { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
        [field: AllowNesting]
        [field: SerializeField, ReadOnly] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public Unit Unit { get; private set; }
        [field: SerializeField] public UnitAttackerData[] AttackerDates { get; private set; }
        [field: SerializeField] public UnitStatData[] Stats { get; private set; }
        
        private readonly List<StatType> _excludedStatTypes = new();
        
        private void Reset()
        {
            RebuildStatsArray();
            RebuildAttackerArray();
        }

        private void OnValidate()
        {
            var requiredStatTypes = GetFilteredStatTypes();
            
            if (Stats == null || Stats.Length != requiredStatTypes.Length)
                RebuildStatsArray();
            
            var requiredStates = GetFilteredStateTypes();
            
            if (AttackerDates == null || AttackerDates.Length != requiredStates.Length)
                RebuildAttackerArray();
            
            LayerMask = LayerMask.GetMask(nameof(Player));
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

        private void RebuildAttackerArray()
        {
            var stateTypes = GetFilteredStateTypes();
            var newAttackers = new UnitAttackerData[stateTypes.Length];

            for (int i = 0; i < stateTypes.Length; i++)
            {
                var currentState = stateTypes[i];
                
                UnitAttackerData existingData = null;
                if (AttackerDates != null)
                {
                    existingData = AttackerDates.FirstOrDefault(a => a != null && a.StateType == currentState);
                }

                if (existingData != null)
                {
                    newAttackers[i] = existingData;
                }
                else
                {
                    newAttackers[i] = new UnitAttackerData(currentState);
                }
            }

            AttackerDates = newAttackers;
        }

        private StateType[] GetFilteredStateTypes()
        {
            return Enum.GetValues(typeof(StateType))
                .Cast<StateType>()
                .Where(stateType => stateType != StateType.None)
                .ToArray();
        }
    }
}
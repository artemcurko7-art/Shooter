using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Type;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Character", fileName = "Character", order = 2)]
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("General")]
        [field: SerializeField] public CharacterType Type { get; private set; }
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        
        [field: Header("Passive Ability")]
        [field: SerializeField] public PassiveAbilityData PassiveAbilityData { get; private set; }
        
        [field: Header("Character")]
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Character View { get; private set; }
        [field: SerializeField] public CharacterStatData[] Stats { get; private set; }

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

            foreach (var stat in Stats)
                stat.OnValidate();
        }

        private void RebuildStatsArray()
        {
            var statTypes = GetFilteredStatTypes();
            var newStats = new CharacterStatData[statTypes.Length];
            
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
            
                newStats[i] = new CharacterStatData(currentType, existingValue);
            }
            
            Stats = newStats;
        }

        private StatType[] GetFilteredStatTypes()
        {
            if (_excludedStatTypes.Contains(StatType.Speed) == false)
                _excludedStatTypes.Add(StatType.Speed);

            if (_excludedStatTypes.Contains(StatType.Distance) == false)
                _excludedStatTypes.Add(StatType.Distance);
            
            return Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .Where(statType => Convert.ToInt32(statType) != 0)
                .Except(_excludedStatTypes)
                .ToArray();
        }
    }
}
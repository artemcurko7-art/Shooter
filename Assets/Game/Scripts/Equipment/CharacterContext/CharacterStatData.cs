using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    [Serializable]
    public class CharacterStatData
    {
        [field: AllowNesting]
        [field: SerializeField, ReadOnly] public StatType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }

        public CharacterStatData(StatType type, int value)
        {
            Type = type;
            Value = value;
        }
        
        public bool IsPercentageValue { get; private set; }

        public void OnValidate()
        {
            IsPercentageValue = Type == StatType.CriticalChance || Type == StatType.CriticalDamage;
        }
    }
}
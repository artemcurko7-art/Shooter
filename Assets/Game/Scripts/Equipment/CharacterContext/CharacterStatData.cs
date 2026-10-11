using System;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    [Serializable]
    public class CharacterStatData
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        
        public bool IsPercentageValue { get; private set; }

        public void OnValidate()
        {
            IsPercentageValue = Type == StatType.CriticalChance || Type == StatType.CriticalDamage;
        }
    }
}
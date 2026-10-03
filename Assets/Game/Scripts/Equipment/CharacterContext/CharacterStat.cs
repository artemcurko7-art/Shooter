using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    [Serializable]
    public class CharacterStat
    {
        [field: SerializeField, ReadOnly] public StatType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        
        public CharacterStat(StatType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}
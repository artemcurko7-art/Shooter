using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    [Serializable]
    public class UnitStatData
    {
        [field: AllowNesting]
        [field: SerializeField, ReadOnly] public StatType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }

        public UnitStatData(StatType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}
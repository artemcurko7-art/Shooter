using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.UnitContext.Data
{
    [Serializable]
    public class UnitStatData
    {
        public const string TypeFieldName = nameof(_type);
        public const string ValueFieldName = nameof(_value);
        
        [SerializeField, ReadOnly] private StatType _type;
        [SerializeField] private int _value;

        public StatType Type => _type;
        public int Value { get => _value; set => _value = value; }

        public UnitStatData(StatType type, int value)
        {
            _type = type;
            _value = value;
        }
    }
}
using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.MV.StatContext.Data
{
    [Serializable]
    public class StatInfoData
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        
        [field: AllowNesting]
        [field: DisableIf(EConditionOperator.Or, nameof(_isActive))]
        [field: SerializeField] public bool IsPercentageValue { get; private set; }

        private bool _isActive => Type == StatType.CriticalChance || Type == StatType.CriticalDamage;

        public void OnValidate()
        {
            if (_isActive)
                IsPercentageValue = true;
        }
    }
}
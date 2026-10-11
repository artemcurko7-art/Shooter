using System;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext
{
    [Serializable]
    public class EquipmentStatData
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        
        [field: AllowNesting]
        [field: DisableIf(EConditionOperator.Or, nameof(IsActive))]
        [field: SerializeField] public bool IsPercentageValue { get; private set; }

        private bool IsActive()
        {
            return Type == StatType.CriticalChance || Type == StatType.CriticalDamage;
        }

        public void OnValidate()
        {
            if (IsActive())
                IsPercentageValue = true;
        }
    }
}
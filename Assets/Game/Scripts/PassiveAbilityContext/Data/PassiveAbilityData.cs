using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Data
{
    [Serializable]
    public class PassiveAbilityData
    {
        [field: SerializeField] public PassiveAbilityType Type { get; private set; }
        [field: SerializeField] public PassiveAbilityTargetingType TargetingType { get; private set; }
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}
using System.Collections.Generic;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public interface ICharacter
    {
        public PassiveAbilityData PassiveAbilityData { get; }
        public IReadOnlyDictionary<StatType, Stat> Stats { get; }
        public Transform Shoot { get; }
        public Transform Transform { get; }
    }
}
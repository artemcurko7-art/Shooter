using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.PassiveAbility.Type;

namespace Game.Scripts.PassiveAbility.Data
{
    public interface IPassiveAbilityData
    {
        public IReadOnlyDictionary<PassiveAbilityType, PassiveAbilityConfig> PassiveAbilities { get; }
    }
}
using System.Collections.Generic;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Registry
{
    public interface IPassiveAbilityTargetingRegistry
    {
        public IReadOnlyDictionary<PassiveAbilityTargetingType, System.Type> Types { get; }
    }
}
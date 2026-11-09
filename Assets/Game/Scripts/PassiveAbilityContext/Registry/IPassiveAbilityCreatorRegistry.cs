using System.Collections.Generic;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Registry
{
    public interface IPassiveAbilityCreatorRegistry
    {
        public IReadOnlyDictionary<PassiveAbilityType, System.Type> Types { get; }
    }
}
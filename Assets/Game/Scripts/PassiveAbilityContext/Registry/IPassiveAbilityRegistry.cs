using System.Collections.Generic;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Registry
{
    public interface IPassiveAbilityRegistry
    {
        public IReadOnlyDictionary<PassiveAbilityType, System.Type> Types { get; }
    }
}
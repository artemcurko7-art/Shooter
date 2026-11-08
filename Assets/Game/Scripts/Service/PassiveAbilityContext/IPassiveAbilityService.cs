using System.Collections.Generic;
using Game.Scripts.PassiveAbilityContext;

namespace Game.Scripts.Service.PassiveAbilityContext
{
    public interface IPassiveAbilityService
    {
        public IReadOnlyList<PassiveAbility> Effects { get; }
    }
}
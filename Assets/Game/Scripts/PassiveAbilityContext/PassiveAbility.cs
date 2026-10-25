using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Targeting;

namespace Game.Scripts.PassiveAbilityContext
{
    public class PassiveAbility
    {
        private readonly IPassiveAbility _passive;
        private readonly IPassiveAbilityTargeting _abilityTargeting;
        
        public PassiveAbility(IPassiveAbility passive, IPassiveAbilityTargeting abilityTargeting)
        {
            _passive = passive;
            _abilityTargeting = abilityTargeting;
        }

        public void Apply(Character owner, IReadOnlyList<Character> characters)
        {
            foreach (var character in _abilityTargeting.GetTargets(characters, owner))
                _passive.Apply(character);
        }
    }
}
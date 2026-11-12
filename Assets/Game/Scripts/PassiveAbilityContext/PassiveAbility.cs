using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Targeting;

namespace Game.Scripts.PassiveAbilityContext
{
    public class PassiveAbility
    {
        private readonly IPassiveAbilityTargeting _abilityTargeting;
        
        public PassiveAbility(IPassiveAbility effect, IPassiveAbilityTargeting abilityTargeting)
        {
            Effect = effect;
            _abilityTargeting = abilityTargeting;
        }
        
        public IPassiveAbility Effect { get; }

        public void Apply(Attacker owner, IReadOnlyList<Attacker> characters, PassiveAbilityData passiveAbilityData)
        {
            foreach (var character in _abilityTargeting.GetTargets(characters, owner))
                Effect.Apply(character, passiveAbilityData);
        }
    }
}
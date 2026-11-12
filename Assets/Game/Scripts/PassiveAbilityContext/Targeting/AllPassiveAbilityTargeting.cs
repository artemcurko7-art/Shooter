using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Targeting
{
    [PassiveAbilityTargetingType(PassiveAbilityTargetingType.All)]
    public class AllPassiveAbilityTargeting : IPassiveAbilityTargeting
    {
        public IEnumerable<Attacker> GetTargets(IReadOnlyList<Attacker> characters, Attacker owner)
        {
            return characters;
        }
    }
}
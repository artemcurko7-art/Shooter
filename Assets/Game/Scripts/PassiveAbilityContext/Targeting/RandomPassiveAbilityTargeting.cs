using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Targeting
{
    [PassiveAbilityTargetingType(PassiveAbilityTargetingType.Random)]
    public class RandomPassiveAbilityTargeting : IPassiveAbilityTargeting
    {
        public IEnumerable<Attacker> GetTargets(IReadOnlyList<Attacker> characters, Attacker owner)
        {
            return characters.OrderBy(character => UnityEngine.Random.value).Take(characters.Count);
        }
    }
}
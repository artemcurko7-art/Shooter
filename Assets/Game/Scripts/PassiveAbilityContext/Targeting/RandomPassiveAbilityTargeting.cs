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
        public PassiveAbilityTargetingType Type => PassiveAbilityTargetingType.Random;
        
        public IEnumerable<Character> GetTargets(IReadOnlyList<Character> characters, Character owner)
        {
            Debug.Log("Random");
            
            return characters.OrderBy(character => UnityEngine.Random.value).Take(2);
        }
    }
}
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Targeting
{
    [PassiveAbilityTargetingType(PassiveAbilityTargetingType.Global)]
    public class GlobalPassiveAbilityTargeting : IPassiveAbilityTargeting
    {
        public PassiveAbilityTargetingType Type => PassiveAbilityTargetingType.Global;
        
        public IEnumerable<Character> GetTargets(IReadOnlyList<Character> characters, Character owner)
        {
            Debug.Log("Global");
            
            return characters;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Targeting
{
    [PassiveAbilityTargetingType(PassiveAbilityTargetingType.AllExceptSelf)]
    public class AllExceptSelfAbilityTargeting : IPassiveAbilityTargeting
    {
        public IEnumerable<Character> GetTargets(IReadOnlyList<Character> characters, Character owner)
        {
            List<Character> newCharacters = new List<Character>(characters);

            foreach (var character in newCharacters.ToList())
                if (character == owner)
                    newCharacters.Remove(character);

            return newCharacters;
        }
    }
}
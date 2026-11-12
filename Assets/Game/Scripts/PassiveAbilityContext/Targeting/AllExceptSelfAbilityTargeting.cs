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
        public IEnumerable<Attacker> GetTargets(IReadOnlyList<Attacker> characters, Attacker owner)
        {
            List<Attacker> newCharacters = new List<Attacker>(characters);

            foreach (var character in newCharacters.ToList())
                if (character == owner)
                    newCharacters.Remove(character);

            return newCharacters;
        }
    }
}
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Targeting
{
    public interface IPassiveAbilityTargeting
    {
        public IEnumerable<Character> GetTargets(IReadOnlyList<Character> characters, Character owner);
    }
}
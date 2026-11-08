using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Repository
{
    public interface IPassiveAbilityDataRepository
    {
        public IReadOnlyDictionary<CharacterType, PassiveAbilityData> PassiveAbilityDates { get; }
    }
}
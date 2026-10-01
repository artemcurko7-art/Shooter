using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Provider
{
    public interface ICharacterProvider
    {
        public IReadOnlyDictionary<CharacterPlaceDropSlotType, Character> Characters { get; }
    }
}
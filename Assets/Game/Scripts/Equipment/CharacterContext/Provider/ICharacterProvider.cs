using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Provider
{
    public interface ICharacterProvider
    {
        public IReadOnlyDictionary<SquadNumberType, CharacterType> Characters { get; }
    }
}
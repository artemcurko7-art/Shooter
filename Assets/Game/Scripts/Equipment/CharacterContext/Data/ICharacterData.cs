using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Data
{
    public interface ICharacterData
    {
        public IReadOnlyDictionary<CharacterType, CharacterConfig> Characters { get; }
    }
}
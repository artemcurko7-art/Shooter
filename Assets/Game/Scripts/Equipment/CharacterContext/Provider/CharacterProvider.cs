using System;
using System.Collections.Generic;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Provider
{
    public class CharacterProvider : ICharacterProvider
    {
        private readonly Dictionary<SquadNumberType, Character> _characters = new();

        private CharacterProvider()
        {
            foreach (var type in Enum.GetValues(typeof(SquadNumberType)))
                _characters.Add((SquadNumberType)type, null);
        }
        
        public IReadOnlyDictionary<SquadNumberType, Character> Characters => _characters;
        
        public void Set(SquadNumberType type, Character character)
        {
            _characters[type] = character;
        }

        public void Remove(SquadNumberType type)
        {
            _characters.Remove(type);
        }
    }
}
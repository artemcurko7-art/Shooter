using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Provider
{
    public class CharacterProvider : ICharacterProvider
    {
        private readonly Dictionary<CharacterPlaceDropSlotType, Character> _characters = new();

        private CharacterProvider()
        {
            foreach (var type in Enum.GetValues(typeof(CharacterPlaceDropSlotType)))
            {
                _characters.Add((CharacterPlaceDropSlotType)type, null);
            }
        }
        
        public IReadOnlyDictionary<CharacterPlaceDropSlotType, Character> Characters => _characters;
        
        public void Set(CharacterPlaceDropSlotType type, Character character)
        {
            _characters[type] = character;
        }

        public void Remove(CharacterPlaceDropSlotType type)
        {
            _characters.Remove(type);
        }
    }
}
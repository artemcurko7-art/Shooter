using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext.Provider
{
    public class CharacterProvider : ICharacterProvider
    {
        private readonly Dictionary<SquadNumberType, CharacterType> _characters = new();

        private CharacterProvider()
        {
            foreach (var type in Enum.GetValues(typeof(SquadNumberType)))
                _characters.Add((SquadNumberType)type, CharacterType.None);
        }
        
        public IReadOnlyDictionary<SquadNumberType, CharacterType> Characters => _characters;
        
        public void Set(SquadNumberType squadNumberType, CharacterType type)
        {
            _characters[squadNumberType] = type;
        }

        public void Remove(SquadNumberType squadNumberType)
        {
            _characters.Remove(squadNumberType);
        }
    }
}
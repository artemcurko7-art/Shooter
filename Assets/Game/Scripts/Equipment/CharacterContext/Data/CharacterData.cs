using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.SquadContext.Type;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext.Data
{
    public class CharacterData : ICharacterData
    {
        private readonly CharacterConfig[] _configs;
        private readonly Dictionary<CharacterType, CharacterConfig> _characters = new();
        
        public CharacterData(CharacterProvider provider)
        {
            _configs = Resources.LoadAll<CharacterConfig>("Configs/Characters");
            
            Fill();
            
            provider.Set(SquadNumberType.First, CharacterType.AttackAircraft);
        }

        public IReadOnlyDictionary<CharacterType, CharacterConfig> Characters => _characters;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == CharacterType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_characters.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");

                _characters.Add(config.Type, config);
            }
        }
    }
}
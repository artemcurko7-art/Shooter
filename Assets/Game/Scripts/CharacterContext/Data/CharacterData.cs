using System;
using System.Collections.Generic;
using Game.Scripts.CharacterContext.Type;
using Game.Scripts.Configs;
using UnityEngine;

namespace Game.Scripts.CharacterContext.Data
{
    public class CharacterData
    {
        private readonly CharacterConfig[] _configs;
        private readonly Dictionary<CharacterType, CharacterConfig> _characters = new();
        
        public CharacterData()
        {
            _configs = Resources.LoadAll<CharacterConfig>("Configs/Characters");
            
            Fill();
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
using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.GameWorldContext.Type;
using UnityEngine;

namespace Game.Scripts.GameWorldContext.Data
{
    public class GameWorldData
    {
        private readonly GameWorldConfig[] _configs;
        private readonly Dictionary<GameWorldType, GameWorldConfig> _worlds = new();
        
        public GameWorldData()
        {
            _configs = Resources.LoadAll<GameWorldConfig>("Configs/GameWorld");
            
            Fill();
        }

        public IReadOnlyDictionary<GameWorldType, GameWorldConfig> Worlds => _worlds;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == GameWorldType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_worlds.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");

                _worlds.Add(config.Type, config);
            }
        }
    }
}
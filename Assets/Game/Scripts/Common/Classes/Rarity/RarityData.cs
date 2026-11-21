using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Type;
using UnityEngine;

namespace Game.Scripts.Common.Classes.Rarity
{
    public class RarityData : IRarityData
    {
        private readonly RarityConfig[] _configs;
        private readonly Dictionary<RarityType, RarityConfig> _rarities = new();
        
        public RarityData()
        {
            _configs = Resources.LoadAll<RarityConfig>("Configs/Rarity");
            
            Fill();
        }
        
        public IReadOnlyDictionary<RarityType, RarityConfig> Rarities => _rarities;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == RarityType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_rarities.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
                
                _rarities.Add(config.Type, config);
            }
        }
    }
}
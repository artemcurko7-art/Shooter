using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext.Data
{
    public class RarityData
    {
        private readonly RarityConfig[] _configs;
        private readonly Dictionary<RarityEquipmentType, RarityConfig> _rarityConfigs = new();
        
        public RarityData()
        {
            _configs = Resources.LoadAll<RarityConfig>("Configs/RarityEquipment");
            
            Fill();
        }
        
        public IReadOnlyDictionary<RarityEquipmentType, RarityConfig> Configs => _rarityConfigs;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == RarityEquipmentType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_rarityConfigs.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
                
                _rarityConfigs.Add(config.Type, config);
            }
        }
    }
}
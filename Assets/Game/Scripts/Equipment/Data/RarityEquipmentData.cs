using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.Data
{
    public class RarityEquipmentData
    {
        private readonly RarityEquipmentConfig[] _configs;
        private readonly Dictionary<RarityEquipmentType, RarityEquipmentConfig> _rarityConfigs = new();
        
        public RarityEquipmentData()
        {
            _configs = Resources.LoadAll<RarityEquipmentConfig>("Configs/RarityEquipment");
            
            Fill();
        }
        
        public IReadOnlyDictionary<RarityEquipmentType, RarityEquipmentConfig> Configs => _rarityConfigs;
        
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
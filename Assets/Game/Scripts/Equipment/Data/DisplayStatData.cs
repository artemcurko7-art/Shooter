using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.Data
{
    public class DisplayStatData
    {
        private readonly DisplayStatConfig[] _configs;
        private readonly Dictionary<StatType, DisplayStatConfig> _stats = new();
        
        public DisplayStatData()
        {
            _configs = Resources.LoadAll<DisplayStatConfig>("Configs/Stat");
            
            Fill();
        }

        public IReadOnlyDictionary<StatType, DisplayStatConfig> Stats => _stats;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == StatType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_stats.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
                
                _stats.Add(config.Type, config);
            }
        }
    }
}
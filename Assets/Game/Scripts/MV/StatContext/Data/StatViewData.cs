using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.MV.StatContext.Data
{
    public class StatViewData : IStatViewData
    {
        private readonly StatViewConfig[] _configs;
        private readonly Dictionary<StatType, StatViewConfig> _stats = new();
        
        public StatViewData()
        {
            _configs = Resources.LoadAll<StatViewConfig>("Configs/Stat");
            
            Fill();
        }

        public IReadOnlyDictionary<StatType, StatViewConfig> Stats => _stats;
        
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
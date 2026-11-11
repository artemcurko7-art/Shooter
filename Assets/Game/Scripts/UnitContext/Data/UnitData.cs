using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Data
{
    public class UnitData : IUnitData
    {
        private readonly UnitConfig[] _configs;
        private readonly Dictionary<UnitType, List<UnitConfig>> _units = new();
    
        public UnitData()
        {
            _configs = Resources.LoadAll<UnitConfig>("Configs/Unit");
            
            Fill();
        }

        public IReadOnlyDictionary<UnitType, List<UnitConfig>> Units => _units;
    
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == UnitType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");
            
                if (_units.ContainsKey(config.Type) == false)
                    _units.Add(config.Type, new List<UnitConfig>());
            
                _units[config.Type].Add(config);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Data
{
    public class UnitData 
    {
        private readonly UnitConfig[] _configs;
        private readonly IUnitAttacker[] _attackers;
        private readonly Dictionary<UnitType, List<UnitConfig>> _units = new();
        private readonly Dictionary<UnitAttackerType, IUnitAttacker> _unitAttackers = new();
    
        public UnitData(IUnitAttacker[] attackers)
        {
            _attackers = attackers;
        
            _configs = Resources.LoadAll<UnitConfig>("Configs/Unit");
            Fill();
        }

        public IReadOnlyDictionary<UnitType, List<UnitConfig>> Units => _units;
        public IReadOnlyDictionary<UnitAttackerType, IUnitAttacker> UnitAttackers => _unitAttackers;
    
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
        
            foreach (var attacker in _attackers)
            {
                if (attacker.Type == UnitAttackerType.None)
                    throw new InvalidOperationException($"Not type: {attacker.Type}");

                if (_unitAttackers.ContainsKey(attacker.Type))
                    throw new InvalidOperationException($"Duplicate type: {attacker.Type}");
            
                _unitAttackers.Add(attacker.Type, attacker);
            }
        }
    }
}
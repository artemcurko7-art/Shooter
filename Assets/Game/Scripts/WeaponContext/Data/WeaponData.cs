using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Data
{
    public class WeaponData
    {
        private readonly WeaponConfig[] _configs;
        private readonly Dictionary<WeaponType, WeaponConfig> _weapons = new();
    
        public WeaponData()
        {
            _configs = Resources.LoadAll<WeaponConfig>("Configs/Weapon");
            
            Fill();
        }
    
        public IReadOnlyDictionary<WeaponType, WeaponConfig> Weapons => _weapons;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == WeaponType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_weapons.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
            
                _weapons.Add(config.Type, config);
            }
        }
    }
}
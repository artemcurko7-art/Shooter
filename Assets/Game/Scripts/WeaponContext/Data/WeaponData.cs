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
        private readonly IWeaponShooting[] _weaponShootings;
        private readonly Dictionary<WeaponType, List<WeaponConfig>> _weapons = new();
        private readonly Dictionary<ShootingType, IWeaponShooting> _shootings = new();
    
        public WeaponData(IWeaponShooting[] weaponShootings)
        {
            _weaponShootings = weaponShootings;
            
            _configs = Resources.LoadAll<WeaponConfig>("Configs/Weapon");
            Fill();
        }
    
        public IReadOnlyDictionary<WeaponType, List<WeaponConfig>> Weapons => _weapons;
        public IReadOnlyDictionary<ShootingType, IWeaponShooting> Shootings => _shootings;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == WeaponType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_weapons.ContainsKey(config.Type) == false)
                    _weapons.Add(config.Type, new List<WeaponConfig>());
            
                _weapons[config.Type].Add(config);
            }
            
            foreach (var weaponShooting in _weaponShootings)
            {
                if (weaponShooting.Type == ShootingType.None)
                    throw new InvalidOperationException($"Not type: {weaponShooting.Type}");

                if (_shootings.ContainsKey(weaponShooting.Type))
                    throw new InvalidOperationException($"Duplicate type: {weaponShooting.Type}");
            
                _shootings.Add(weaponShooting.Type, weaponShooting);
            }
        }
    }
}
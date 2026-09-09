using System;
using System.Collections.Generic;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Data
{
    public class WeaponShootingData
    {
        private readonly IWeaponShooting[] _weaponShootings;
        private readonly Dictionary<ShootingType, IWeaponShooting> _shootings = new();
    
        public WeaponShootingData(IWeaponShooting[] weaponShootings)
        {
            _weaponShootings = weaponShootings;
            
            Fill();
        }
        
        public IReadOnlyDictionary<ShootingType, IWeaponShooting> Shootings => _shootings;

        private void Fill()
        {
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
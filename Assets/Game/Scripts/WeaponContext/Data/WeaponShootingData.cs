using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.WeaponContext.Attribute;
using Game.Scripts.WeaponContext.Shooting;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Data
{
    public class WeaponShootingData
    {
        private readonly Dictionary<ShootingType, System.Type> _shootings = new();
        
        public WeaponShootingData()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<ShootingType, System.Type> Shootings => _shootings;
        
        private void Fill()
        {
            var types = ReflectionExtensions.GetImplementations<IWeaponShooting>();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<WeaponShootingTypeAttribute>();
            
                if (attribute == null)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' implements IWeaponShooting but is missing [ShootingType] attribute!");
                }

                ShootingType shootingType = attribute.Type;

                if (shootingType == ShootingType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to ShootingType.None!");
                }

                if (_shootings.TryAdd(shootingType, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate ShootingType registration for '{shootingType}'. " +
                        $"Conflict between '{_shootings[shootingType].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.WeaponContext.Attribute;
using Game.Scripts.WeaponContext.Shooting;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Data
{
    public class WeaponShootingData
    {
        private readonly Dictionary<ShootingType, System.Type> _types = new();
        
        public WeaponShootingData()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<ShootingType, System.Type> Types => _types;
        
        private void Fill()
        {
            foreach (var type in ReflectionExtensions.GetImplementations<IWeaponShooting>())
            {
                var attribute = type.GetCustomAttribute<WeaponShootingTypeAttribute>();

                if (attribute.Type == ShootingType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to ShootingType.None!");
                }

                if (_types.TryAdd(attribute.Type, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate ShootingType registration for '{attribute.Type}'. " +
                        $"Conflict between '{_types[attribute.Type].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
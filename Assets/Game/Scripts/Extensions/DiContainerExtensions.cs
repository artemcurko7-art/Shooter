using System;
using Game.Scripts.Configs;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono;
using Game.Scripts.ProjectileContext;
using Game.Scripts.VFX;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Extensions
{
    public static class DiContainerExtensions
    {
        public static PassiveAbilityViewPool InstantiatePassiveAbilityViewPool(this DiContainer container, PassiveAbilityData data, Transform transform)
        {
            return container.Instantiate<PassiveAbilityViewPool>(new object[] { data, transform });
        }
        
        public static UnitPool InstantiateUnitPool(this DiContainer container, UnitConfig config)
        {
            return container.Instantiate<UnitPool>(new object[] { config });
        }
        
        public static ProjectilePool InstantiateProjectilePool(this DiContainer container, Projectile projectile)
        {
            return container.Instantiate<ProjectilePool>(new object[] { projectile });
        }
        
        public static EffectPool InstantiateEffectPool(this DiContainer container, Effect effect)
        {
            return container.Instantiate<EffectPool>(new object[] { effect });
        }
        
        public static IWeaponShooting InstantiateWeaponShooting(this DiContainer container, Type type, IWeaponShootingConfig config, ProjectilePool projectilePool)
        {
            return (IWeaponShooting)container.Instantiate(type, new object[] { config, projectilePool });
        }
    }
}
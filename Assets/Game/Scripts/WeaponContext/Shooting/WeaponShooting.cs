using System;
using Game.Scripts.Attacked;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Shooting
{
    public abstract class WeaponShooting
    {
        public WeaponShooting(IWeaponShootingConfig config, TrackerUnits trackerUnits, ProjectilePool projectilePool)
        {
            Config = config;
            TrackerUnits = trackerUnits;
            ProjectilePool = projectilePool;
        }

        public ShootingType Type => GetType();
        protected IWeaponShootingConfig Config { get; }
        protected TrackerUnits TrackerUnits { get; }
        protected ProjectilePool ProjectilePool { get; }

        protected abstract ShootingType GetType();
    }
}
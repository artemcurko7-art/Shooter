using System;
using System.Collections.Generic;
using Game.Scripts.HitImpacted;
using Game.Scripts.WeaponContext;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class ProjectilePool : PoolMono<Projectile>
    {
        public event Action<IImpactReceiver> Added;
        public event Action<IImpactReceiver> Removed;
        
        public ProjectilePool(DiContainer container) : base(container) { }
        
        protected override void ActionOnGet(Projectile projectile)
        {
            base.ActionOnGet(projectile);
            Added?.Invoke(projectile);
            projectile.Released += OnRelease;
        }

        protected override void ActionOnRelease(Projectile projectile)
        {
            base.ActionOnRelease(projectile);
            projectile.ResetSettings();
        }

        protected override void OnRelease(Projectile projectile)
        {
            base.OnRelease(projectile);
            Removed?.Invoke(projectile);
            projectile.Released -= OnRelease;
        }
    }
}
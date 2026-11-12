using System;
using Game.Scripts.HitImpacted;
using Game.Scripts.HitImpacted.HitSources;
using Game.Scripts.ProjectileContext;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class ProjectilePool : PoolMono<Projectile>, IHitSourceRemoved
    {
        public event Action<IImpactReceiver> Added;
        public event Action<IImpactReceiver> Removed;

        public ProjectilePool(Projectile projectile, DiContainer container) : base(container)
        {
            SetPrefab(projectile);
        }
        
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
using System;
using System.Collections.Generic;
using Game.Scripts.HitImpacted;
using Game.Scripts.WeaponContext;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class BulletPool : PoolMono<Bullet>
    {
        public event Action<IImpactReceiver> Added;
        public event Action<IImpactReceiver> Removed;
        
        public BulletPool(DiContainer container) : base(container) { }
        
        protected override void ActionOnGet(Bullet bullet)
        {
            base.ActionOnGet(bullet);
            Added?.Invoke(bullet);
            bullet.Released += OnRelease;
        }

        protected override void ActionOnRelease(Bullet bullet)
        {
            base.ActionOnRelease(bullet);
            bullet.ResetSettings();
        }

        protected override void OnRelease(Bullet bullet)
        {
            base.OnRelease(bullet);
            Removed?.Invoke(bullet);
            bullet.Released -= OnRelease;
        }
    }
}
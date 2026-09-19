using Game.Scripts.PoolMono;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public abstract class HitImpactedObserver : ISubscriber
    {
        private readonly BulletPool _bulletPool;
        
        public HitImpactedObserver(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
        
        public void Subscribe()
        {
            _bulletPool.Added += OnAdded;
            _bulletPool.Removed += OnRemoved;
        }

        public void Unsubscribe()
        {
            _bulletPool.Added -= OnAdded;
            _bulletPool.Removed -= OnRemoved;
        }

        protected abstract void OnHitImpacted(RaycastHit hit);
        
        private void OnAdded(IImpactReceiver receiver)
        {
            receiver.HitImpacted += OnHitImpacted;
        }
        
        private void OnRemoved(IImpactReceiver receiver)
        {
            receiver.HitImpacted -= OnHitImpacted;
        }
    }
}
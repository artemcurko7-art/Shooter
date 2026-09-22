using Game.Scripts.PoolMono;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public abstract class HitImpactedObserver : ISubscriber
    {
        private readonly ProjectilePool _projectilePool;
        
        public HitImpactedObserver(ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
        }
        
        public void Subscribe()
        {
            _projectilePool.Added += OnAdded;
            _projectilePool.Removed += OnRemoved;
        }

        public void Unsubscribe()
        {
            _projectilePool.Added -= OnAdded;
            _projectilePool.Removed -= OnRemoved;
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
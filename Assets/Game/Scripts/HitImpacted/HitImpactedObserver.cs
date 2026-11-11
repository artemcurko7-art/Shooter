using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.ProjectileContext.Type;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public abstract class HitImpactedObserver : ISubscriber
    {
        private readonly IProjectilePoolRepository _projectilePoolRepository;
        
        public HitImpactedObserver(IProjectilePoolRepository projectilePoolRepository)
        {
            _projectilePoolRepository = projectilePoolRepository;
        }
        
        public void Subscribe()
        {
            _projectilePoolRepository.Pools[ProjectileType.Default].Added += OnAdded;
            _projectilePoolRepository.Pools[ProjectileType.Default].Removed += OnRemoved;
        }

        public void Unsubscribe()
        {
            _projectilePoolRepository.Pools[ProjectileType.Default].Added -= OnAdded;
            _projectilePoolRepository.Pools[ProjectileType.Default].Removed -= OnRemoved;
        }

        protected abstract void OnHitImpacted(HitContext hitContext);
        
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
using Game.Scripts.PoolMono;
using Game.Scripts.VFX;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class EffectHitImpacted : HitImpactedObserver
    {
        private readonly EffectPool _pool;
        private readonly Effect _effect;
        
        public EffectHitImpacted(EffectPool pool, Effect effect, BulletPool bulletPool) : base(bulletPool)
        {
            _pool = pool;
            _effect = effect;
            
            _pool.SetPrefab(effect);
        }
        
        protected override void OnHitImpacted(RaycastHit hit)
        {
            var obj = _pool.Get();
            obj.Initialize(hit.point);
            obj.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }
}
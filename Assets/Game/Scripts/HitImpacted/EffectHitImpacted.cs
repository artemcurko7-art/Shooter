using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.VFX;
using Game.Scripts.VFX.Data;
using Game.Scripts.VFX.Type;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class EffectHitImpacted : HitImpactedObserver
    {
        private readonly IEffectPoolRepository _repository;
        
        public EffectHitImpacted(IEffectPoolRepository repository, ProjectilePool projectilePool) : base(projectilePool)
        {
            _repository = repository;
        }
        
        protected override void OnHitImpacted(HitContext hitContext)
        {
            var obj = _repository.Pools[EffectType.BloodBurst].Get();
            obj.Initialize(hitContext.RaycastHit.point);
            obj.transform.rotation = Quaternion.LookRotation(hitContext.RaycastHit.normal);
        }
    }
}
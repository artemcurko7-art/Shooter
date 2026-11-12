using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.VFX.Type;

namespace Game.Scripts.HitImpacted
{
    public class EffectHitImpacted : HitImpactedObserver
    {
        private readonly IEffectPoolRepository _repository;
        
        public EffectHitImpacted(
            IEffectPoolRepository repository,
            IHitSourceRegistry hitSourceRegistry)
            : base(hitSourceRegistry)
        {
            _repository = repository;
        }

        protected override void OnHitImpacted(HitContext hitContext)
        {
            var obj = _repository.Pools[EffectType.BloodBurst].Get();
            obj.Initialize(hitContext.RaycastHit.point);
        }
    }
}
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.Equipment.CharacterContext;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class DamageTextPopupHitImpacted : HitImpactedObserver
    {
        private const float Multiplier = 2.5f;
        private readonly ICharacterService _characterService;
        private readonly DamageTextPopupPool _pool;
        private readonly Camera _mainCamera;

        public DamageTextPopupHitImpacted(
            ICharacterService characterService,
            DamageTextPopupPool pool,
            Camera mainCamera,
            ProjectilePool projectilePool)
            : base(projectilePool)
        { 
            _characterService = characterService;
            _pool = pool; 
            //_statRepository = statRepository;
            _mainCamera = mainCamera;
        }

        protected override void OnHitImpacted(HitContext hitContext)
        {
            if (hitContext.RaycastHit.transform.TryGetComponent(out Unit unit))
            {
                var value = _pool.Get();
                value.Initialize(hitContext.RaycastHit.point);
                value.Initialize(_mainCamera, hitContext.ShotContext.Damage, hitContext.ShotContext.IsCritical);
            }
        }
    }
}
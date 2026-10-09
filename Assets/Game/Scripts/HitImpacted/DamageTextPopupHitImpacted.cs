using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class DamageTextPopupHitImpacted : HitImpactedObserver
    {
        private readonly DamageTextPopupPool _pool;
        private readonly StatRepository _statRepository;
        private readonly Camera _mainCamera;

        public DamageTextPopupHitImpacted(
            DamageTextPopupPool pool,
            StatRepository statRepository,
            Camera mainCamera,
            ProjectilePool projectilePool)
            : base(projectilePool)
        {
           _pool = pool; 
           _statRepository = statRepository;
           _mainCamera = mainCamera;
        }

        protected override void OnHitImpacted(HitContext hitContext)
        {
            if (hitContext.RaycastHit.transform.TryGetComponent(out Unit unit))
            {
                Debug.Log($"Выстрелил: {hitContext.Attacker.CharacterType}, Попал в {unit.name}");
            }
            
            // var value = _pool.Get();
            // value.Initialize(hit.point);
            // int value2 = UserUtils.NumberGeneration.GetIntegerRandom(1, 2);
            // bool isCritical = value2 == 1;
            // value.Initialize(_mainCamera, UserUtils.NumberGeneration.GetIntegerRandom(50, 550), isCritical);
        }
    }
}
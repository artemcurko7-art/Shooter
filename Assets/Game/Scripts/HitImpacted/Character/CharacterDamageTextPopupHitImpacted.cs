using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.HitImpacted.Character
{
    public class CharacterDamageTextPopupHitImpacted : CharacterHitImpactedObserver
    {
        private readonly DamageTextPopupPool _pool;
        private readonly Camera _mainCamera;
        
        public CharacterDamageTextPopupHitImpacted(
            DamageTextPopupPool pool,
            Camera mainCamera,
            IHitSourceRegistry hitSourceRegistry)
            : base(hitSourceRegistry)
        {
            _pool = pool; 
            _mainCamera = mainCamera;
        }

        protected override void OnHitImpacted(HitContext hitContext)
        {
            var value = _pool.Get();
            value.Initialize(hitContext.RaycastHit.point);
            value.Initialize(_mainCamera, hitContext.ShotContext.Damage, hitContext.ShotContext.IsCritical);
        }
    }
}
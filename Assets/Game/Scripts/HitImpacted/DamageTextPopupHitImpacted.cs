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
        private readonly CharacterStatsRepository _statRepository;
        private readonly Camera _mainCamera;

        public DamageTextPopupHitImpacted(
            ICharacterService characterService,
            DamageTextPopupPool pool,
            //CharacterStatsRepository statRepository,
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
                // int health = _characterService.Characters[0].Stats[0].Value;
                // int damage = _characterService.Characters[0].Stats[1].Value;
                // int defence = _characterService.Characters[0].Stats[2].Value;
                // int criticalChance = _characterService.Characters[0].Stats[3].Value;
                // int criticalDamage = _characterService.Characters[0].Stats[4].Value;
                //
                // //CritRoller critRoller = new CritRoller(criticalChance);
                //
                // bool isCrit = _criticalDamageResolver.Resolve();
                //
                // var value = _pool.Get();
                // value.Initialize(hitContext.RaycastHit.point);
                //
                // float currentDamage = damage * Multiplier;
                //
                // if (isCrit)
                // {
                //     float calculation = currentDamage * (1f + criticalDamage / 100f);
                //     currentDamage = calculation;
                // }
                //
                // value.Initialize(_mainCamera, Mathf.CeilToInt(currentDamage), isCrit);
            }
        }
    }
}
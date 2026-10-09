using DG.Tweening;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class KnockbackHitImpacted : HitImpactedObserver
    {
        private const float From = 5;
        private const float To = 0;
        private const float Duration = 0.5f;
        
        public KnockbackHitImpacted(ProjectilePool projectilePool) : base(projectilePool) { }

        protected override void OnHitImpacted(HitContext hitContext)
        {
            // if (hitContext.RaycastHit.transform.TryGetComponent<CharacterController>(out var characterController))
            // {
            //     DOVirtual.Float(From, To, Duration, currentForce =>
            //     {
            //         characterController.Move(-hitContext.RaycastHit.transform.forward * (currentForce * Time.deltaTime));
            //     }).SetEase(Ease.OutQuad);
            // }
        }
    }
}
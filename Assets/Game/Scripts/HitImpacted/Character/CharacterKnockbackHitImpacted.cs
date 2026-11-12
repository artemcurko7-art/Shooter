using Game.Scripts.HitImpacted.Registry;

namespace Game.Scripts.HitImpacted.Character
{
    public class CharacterKnockbackHitImpacted : CharacterHitImpactedObserver
    {
        private const float From = 5;
        private const float To = 0;
        private const float Duration = 0.5f;
        
        public CharacterKnockbackHitImpacted(IHitSourceRegistry hitSourceRegistry) : base(hitSourceRegistry) { }

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
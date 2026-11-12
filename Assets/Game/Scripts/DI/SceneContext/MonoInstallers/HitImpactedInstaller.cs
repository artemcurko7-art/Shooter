using Game.Scripts.Factory;
using Game.Scripts.HitImpacted;
using Game.Scripts.HitImpacted.Character;
using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.HitImpacted.Unit;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.TextPopup;
using Game.Scripts.VFX;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class HitImpactedInstaller : MonoInstaller
    {
        [SerializeField] private DamageTextPopup _damageTextPopup;
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _flash;
        [SerializeField] private float _cooldownFlash;
        
        public override void InstallBindings()
        {
            BindCommon();
            BindCharacter();
            BindUnit();

            Container
                .Bind<IHitSourceRegistry>()
                .To<HitSourceRegistry>()
                .AsSingle();
            
            Container
                .Bind<DamageTextPopupFactory>()
                .AsSingle()
                .WithArguments(_damageTextPopup);
        }

        private void BindCommon()
        {
            Container
                .Bind<ISubscriber>()
                .To<EffectHitImpacted>()
                .AsCached();
            
            Container
                .Bind<ISubscriber>()
                .To<FlashHitImpacted>()
                .AsCached()
                .WithArguments(_flash, _cooldownFlash);
        }

        private void BindCharacter()
        {
            Container
                .Bind<ISubscriber>()
                .To<CharacterDamageTextPopupHitImpacted>()
                .AsCached()
                .WithArguments(_camera);
            
            Container
                .Bind<ISubscriber>()
                .To<CharacterKnockbackHitImpacted>()
                .AsCached();
        }
        
        private void BindUnit()
        {
            // Container
            //     .Bind<ISubscriber>()
            //     .To<UnitHitImpactedObserver>()
            //     .AsCached();
        }
    }
}
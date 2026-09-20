using Game.Scripts.Factory;
using Game.Scripts.HitImpacted;
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
        [SerializeField] private Effect _effect;
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _flash;
        [SerializeField] private float _cooldownFlash;
        
        public override void InstallBindings()
        {
            Container
                .Bind<DamageTextPopupFactory>()
                .AsSingle()
                .WithArguments(_damageTextPopup);
            
            Container
                .Bind<ISubscriber>()
                .To<EffectHitImpacted>()
                .AsCached()
                .WithArguments(_effect);

            Container
                .Bind<ISubscriber>()
                .To<FlashHitImpacted>()
                .AsCached()
                .WithArguments(_flash, _cooldownFlash);

            Container
                .Bind<ISubscriber>()
                .To<DamageTextPopupHitImpacted>()
                .AsCached()
                .WithArguments(_camera);

            Container
                .Bind<ISubscriber>()
                .To<KnockbackHitImpacted>()
                .AsCached();
        }
    }
}
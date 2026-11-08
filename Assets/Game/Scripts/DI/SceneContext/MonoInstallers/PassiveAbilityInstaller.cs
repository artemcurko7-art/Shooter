using Game.Scripts.Factory;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Applied;
using Game.Scripts.PassiveAbilityContext.Repository;
using Game.Scripts.Service.PassiveAbilityContext;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PassiveAbilityInstaller : MonoInstaller
    {
        [SerializeField] private PassiveAbilityView _view;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<PassiveAbilityService>()
                .AsSingle();
            
            Container
                .Bind<PassiveAbilityFactory>()
                .AsSingle();

            Container
                .Bind<PassiveAbilityViewFactory>()
                .AsSingle()
                .WithArguments(_view);
            
            Container
                .Bind<ISubscriber>()
                .To<PassiveAbilityViewApplied>()
                .AsSingle();
            
            Container
                .Bind<ISubscriber>()
                .To<EffectsApplied>()
                .AsSingle();
        }
    }
}
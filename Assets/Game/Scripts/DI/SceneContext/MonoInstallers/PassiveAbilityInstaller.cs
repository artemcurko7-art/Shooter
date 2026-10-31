using Game.Scripts.Factory;
using Game.Scripts.PassiveAbilityContext;
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
                .Bind<ISubscriber>()
                .To<PassiveAbilityService>()
                .AsSingle();
            
            Container
                .Bind<PassiveAbilityFactory>()
                .AsSingle();

            Container
                .Bind<PassiveAbilityViewFactory>()
                .AsSingle()
                .WithArguments(_view);
        }
    }
}
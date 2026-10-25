using Game.Scripts.Factory;
using Game.Scripts.Service.PassiveAbility;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PassiveAbilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PassiveAbilityService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PassiveAbilityFactory>()
                .AsSingle();
        }
    }
}
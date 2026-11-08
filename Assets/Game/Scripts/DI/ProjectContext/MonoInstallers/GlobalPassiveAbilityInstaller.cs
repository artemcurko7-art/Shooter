using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Registry;
using Game.Scripts.PassiveAbilityContext.Repository;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalPassiveAbilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IPassiveAbilityRegistry>()
                .To<PassiveAbilityRegistry>()
                .AsSingle();
            
            Container
                .Bind<IPassiveAbilityTargetingRegistry>()
                .To<PassiveAbilityTargetingRegistry>()
                .AsSingle();
            
            Container
                .Bind<IPassiveAbilityViewData>()
                .To<PassiveAbilityViewData>()
                .AsSingle();
            
            Container
                .Bind<IPassiveAbilityDataRepository>()
                .To<PassiveAbilityDataRepository>()
                .AsSingle();
        }
    }
}
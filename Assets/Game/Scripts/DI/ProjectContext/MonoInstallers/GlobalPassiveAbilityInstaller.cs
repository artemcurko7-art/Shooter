using Game.Scripts.PassiveAbilityContext.Registry;
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
        }
    }
}
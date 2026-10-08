using Game.Scripts.PassiveAbility.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalPassiveAbilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IPassiveAbilityData>()
                .To<PassiveAbilityData>()
                .AsSingle();
        }
    }
}
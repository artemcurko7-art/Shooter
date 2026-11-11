using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Registry;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalUnitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IUnitData>()
                .To<UnitData>()
                .AsSingle();
            
            Container
                .Bind<IUnitAttackerCreatorRegistry>()
                .To<UnitAttackerCreatorRegistry>()
                .AsSingle();
        }
    }
}
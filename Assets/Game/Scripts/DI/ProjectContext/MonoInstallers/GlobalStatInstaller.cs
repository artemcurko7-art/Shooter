using Game.Scripts.MV.StatContext.Repository;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalStatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IStatTypeRegistry>()
                .To<StatTypeRegistry>()
                .AsSingle();
        }
    }
}
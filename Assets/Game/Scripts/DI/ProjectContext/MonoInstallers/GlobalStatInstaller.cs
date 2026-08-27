using Game.Scripts.MV.Stat.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalStatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<StatData>()
                .AsSingle()
                .NonLazy();
        }
    }
}
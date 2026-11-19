using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext.Data;
using Game.Scripts.MV.StatContext.Repository;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalStatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IStatRegistry>()
                .To<StatRegistry>()
                .AsSingle();

            Container
                .Bind<StatFactory>()
                .AsSingle();
            
            Container
                .Bind<IStatViewData>()
                .To<StatViewData>()
                .AsSingle();
        }
    }
}
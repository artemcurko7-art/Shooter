using Game.Scripts.GameWorldContext;
using Game.Scripts.GameWorldContext.Data;
using Game.Scripts.GameWorldContext.Provider;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalGameWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameWorldData>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<GameWorldProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}
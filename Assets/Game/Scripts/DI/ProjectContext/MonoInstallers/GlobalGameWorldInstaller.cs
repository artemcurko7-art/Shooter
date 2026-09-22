using Game.Scripts.GameWorldContext.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalGameWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameWorldData>()
                .AsSingle();
        }
    }
}
using Game.Scripts.MV.StatContext;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class StatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Health>()
                .AsSingle();
        }
    }
}
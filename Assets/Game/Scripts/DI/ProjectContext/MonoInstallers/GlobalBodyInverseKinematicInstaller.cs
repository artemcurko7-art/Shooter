using Game.Scripts.BodyIK;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalBodyInverseKinematicInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BodyInverseKinematicData>()
                .AsSingle();
        }
    }
}
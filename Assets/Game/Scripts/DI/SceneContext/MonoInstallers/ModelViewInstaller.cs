using Game.Scripts.MV.GameProgressContext;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class ModelViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameProgress();
        }

        private void BindGameProgress()
        {
            Container
                .BindInterfacesTo<GameProgress>()
                .AsSingle();
        }
    }
}
using Game.Scripts.GameCoreContext;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class GameCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPaused();
            BindResumed();
            BindEnded();
            
            Container
                .BindInterfacesAndSelfTo<GameCore>() 
                .AsSingle();
        }

        private void BindPaused()
        {
            
        }

        private void BindResumed()
        {
            
        }

        private void BindEnded()
        {
            
        }
    }
}
using Game.Scripts.VFX.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalEffectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IEffectData>()
                .To<EffectData>()
                .AsSingle();
        }
    }
}
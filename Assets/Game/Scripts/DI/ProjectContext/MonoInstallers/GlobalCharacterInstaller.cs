using Game.Scripts.CharacterContext.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalCharacterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CharacterData>()
                .AsSingle();
        }
    }
}
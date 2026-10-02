using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Provider;
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
            
            Container
                .BindInterfacesAndSelfTo<CharacterProvider>()
                .AsSingle();
        }
    }
}
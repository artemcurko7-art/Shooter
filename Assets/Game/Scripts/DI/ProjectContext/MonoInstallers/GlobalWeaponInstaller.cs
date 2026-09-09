using Game.Scripts.Factory;
using Game.Scripts.Provider;
using Game.Scripts.WeaponContext.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalWeaponInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<WeaponData>()
                .AsSingle();
        
            Container
                .Bind<WeaponProvider>()
                .AsSingle();
        }
    }
}
using Game.Scripts.Factory;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Provider;
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
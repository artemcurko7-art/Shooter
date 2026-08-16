using Game.Scripts.Provider;
using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Data;
using Zenject;
using Game.Scripts.WeaponContext.Shooting;
using Single = Game.Scripts.WeaponContext.Shooting.Single;

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
            
            Container
                .Bind<IWeaponShooting>()
                .To<Single>()
                .AsCached();
            
            Container
                .Bind<IWeaponShooting>()
                .To<Multiplier>()
                .AsCached();
            
            Container
                .Bind<IWeaponShooting>()
                .To<Cutting>()
                .AsCached();
        }
    }
}
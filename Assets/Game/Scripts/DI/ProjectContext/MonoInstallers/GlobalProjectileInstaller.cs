using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.ProjectileContext.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IProjectileData>()
                .To<ProjectileData>()
                .AsSingle();
        }
    }
}
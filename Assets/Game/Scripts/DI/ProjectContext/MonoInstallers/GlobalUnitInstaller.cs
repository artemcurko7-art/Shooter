using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalUnitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UnitData>()
                .AsSingle();
        
            // Container
            //     .Bind<UnitFactory>()
            //     .AsSingle();
        
            Container
                .Bind<IUnitAttacker>()
                .To<MeleeAttacker>()
                .AsCached();
        
            Container
                .Bind<IUnitAttacker>()
                .To<AreaDamage>()
                .AsCached();
        }
    }
}
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalUnitInstaller : MonoInstaller
    {
        [SerializeField] private Projectile _projectile;
        
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
                .To<AreaDamageAttacker>()
                .AsCached();
            
            Container
                .Bind<IUnitAttacker>()
                .To<ThrowerAttacker>()
                .AsCached()
                .WithArguments(_projectile);
        }
    }
}
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalUnitInstaller : MonoInstaller
    {
        [SerializeField] private Projectile projectile;
        
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
                .To<Melee>()
                .AsCached();
        
            Container
                .Bind<IUnitAttacker>()
                .To<AreaDamage>()
                .AsCached();
            
            Container
                .Bind<IUnitAttacker>()
                .To<Thrower>()
                .AsCached()
                .WithArguments(projectile);
        }
    }
}
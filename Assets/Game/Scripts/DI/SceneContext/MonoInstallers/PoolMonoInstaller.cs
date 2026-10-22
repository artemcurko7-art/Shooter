using System;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.MapContext;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.Map;
using Game.Scripts.Service.UnitContext;
using Game.Scripts.TextPopup;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PoolMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUnit();
            BindMap();
            BindBullet();
            BindEffect();
            BindDamageTextPopup();
        }

        private void BindUnit()
        {
            foreach (var type in Enum.GetValues(typeof(UnitType)))
            {
                if ((UnitType)type == UnitType.None)
                    continue;
                
                Container
                    .Bind<UnitPool>()
                    .AsCached()
                    .WithArguments((UnitType)type);
            }
            
            Container
                .Bind<UnitFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UnitService>() // add bind interfaces to
                .AsSingle();
        }

        private void BindMap()
        {
            Container
                .Bind<MapPool>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MapService>()
                .AsSingle();
        }
        
        private void BindBullet()
        {
            Container
                .Bind<ProjectilePool>()
                .AsSingle();
        }

        private void BindEffect()
        {
            Container
                .Bind<EffectPool>()
                .AsSingle();
        }

        private void BindDamageTextPopup()
        {
            Container
                .Bind<DamageTextPopupPool>()
                .AsSingle();
        }
    }
}
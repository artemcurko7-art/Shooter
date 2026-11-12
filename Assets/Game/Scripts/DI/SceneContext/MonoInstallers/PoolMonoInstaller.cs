using System;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.PoolMono.Repository.PassiveAbilityView;
using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.PoolMono.Repository.Unit;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Map;
using Game.Scripts.Service.UnitContext;
using Game.Scripts.TextPopup;
using Game.Scripts.UnitContext.Repository;
using Game.Scripts.UnitContext.Type;
using Game.Scripts.VFX.Type;
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
            BindProjectile();
            BindPassiveAbilityView();
            BindEffect();
            BindDamageTextPopup();
        }

        private void BindUnit()
        {
            Container
                .Bind<IUnitPoolRepository>()
                .To<UnitPoolRepository>()
                .AsSingle();
            
            Container
                .Bind<UnitFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UnitService>() // add bind interfaces to
                .AsSingle();
            
            Container
                .BindInterfacesTo<UnitAttackerService>()
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
        
        private void BindProjectile()
        {
            Container
                .BindInterfacesTo<ProjectilePoolRepository>()
                .AsSingle();
        }

        private void BindPassiveAbilityView()
        {
            Container
                .Bind<IPassiveAbilityViewPoolRepository>()
                .To<PassiveAbilityViewPoolRepository>()
                .AsSingle();
        }
        
        private void BindEffect()
        {
            Container
                .Bind<IEffectPoolRepository>()
                .To<EffectPoolRepository>()
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
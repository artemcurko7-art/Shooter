using System;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.MapContext;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.PhysicalBody;
using Game.Scripts.TextPopup;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PoolMonoInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _transforms;
        [SerializeField] private float _delay;
    
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
                .AsSingle()
                .WithArguments(_transforms);
            
            Container
                .BindInterfacesAndSelfTo<UnitService>() // add bind interfaces to
                .AsSingle()
                .WithArguments(_transforms, _delay);
        }

        private void BindMap()
        {
            Container
                .Bind<MapPool>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MapService>()
                .AsSingle()
                .WithArguments(3f);
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
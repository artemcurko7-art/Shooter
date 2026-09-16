using System;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.PhysicalBody;
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
    }
}
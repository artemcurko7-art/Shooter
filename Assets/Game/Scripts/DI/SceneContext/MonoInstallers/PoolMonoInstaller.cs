using Game.Scripts.Factory;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.PhysicalBody;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PoolMonoInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _delay;
    
        public override void InstallBindings()
        {
            BindUnit();
        }

        private void BindUnit()
        {
            Container
                .Bind<UnitPool>()
                .AsSingle();
        
            Container
                .Bind<UnitFactory>()
                .AsSingle();
        
            Container
                .BindInterfacesTo<UnitService>()
                .AsSingle()
                .WithArguments(_transform, _delay);
        }
    }
}
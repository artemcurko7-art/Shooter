using Game.Scripts.Configs;
using Game.Scripts.Factory;
using Game.Scripts.Provider;
using Game.Scripts.Service.Weapon;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class WeaponInstaller : MonoInstaller
    {
        [Header("General")]
        [SerializeField] private Transform _container;
        
        [Header("Single")]
        [SerializeField] private float _cooldownSingle;
        
        private WeaponProvider _provider;
        private WeaponConfig[] _configs; // test
        
        [Inject]
        public void Construct(WeaponProvider provider)
        {
            _provider = provider;
            _configs = Resources.LoadAll<WeaponConfig>("Configs/Weapon"); // test
        }
        
        public override void InstallBindings()
        {
            Container
                .Bind<WeaponShootingData>()
                .AsSingle();
            
            Container
                .Bind<WeaponService>()
                .AsSingle()
                .WithArguments(_container)
                .NonLazy();
            
            Container
                .Bind<WeaponViewFactory>()
                .AsSingle();
            
            // Container
            //     .Bind<WeaponConfig>()
            //     .FromInstance(_provider.Config)
            //     .AsSingle();
            
            Container // test
                .Bind<WeaponConfig>()
                .FromInstance(_configs[2])
                .AsSingle();
            
            Container
                .BindInterfacesTo<Single>()
                .AsCached()
                .WithArguments(_cooldownSingle);
            
            Container
                .BindInterfacesTo<Multiplier>()
                .AsCached();
            
            Container
                .BindInterfacesTo<Cutting>()
                .AsCached();
        }
    }
}
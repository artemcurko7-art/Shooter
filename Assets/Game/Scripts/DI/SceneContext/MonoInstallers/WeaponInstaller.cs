using Game.Scripts.Configs;
using Game.Scripts.Provider;
using Game.Scripts.Service.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private Transform _container;
        
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
            // Container
            //     .Bind<WeaponConfig>()
            //     .FromInstance(_provider.Config)
            //     .AsSingle();

            Container // test
                .Bind<WeaponConfig>()
                .FromInstance(_configs[2])
                .AsSingle();
            
            Container
                .Bind<WeaponService>()
                .AsSingle()
                .WithArguments(_container)
                .NonLazy();
        }
    }
}
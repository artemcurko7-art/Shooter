using Game.Scripts.Configs;
using Game.Scripts.Factory;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.Service.Weapon
{
    public class WeaponService
    {
        private readonly WeaponConfig _config;
        private readonly WeaponViewFactory _factory;
        private readonly Transform _container;
        
        public WeaponService(WeaponConfig config, WeaponViewFactory factory, Transform container)
        {
            _container = container;
            _config = config;
            _factory = factory;
            
            Create();
        }

        public WeaponView View { get; private set; }
        
        private void Create()
        {
            View = _factory.Create(_config, _container);
        }
    }
}
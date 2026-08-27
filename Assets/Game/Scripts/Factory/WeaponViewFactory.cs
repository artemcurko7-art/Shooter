using Game.Scripts.Configs;
using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class WeaponViewFactory
    {
        private readonly IWeaponShootingData _shootingData;
        private readonly DiContainer _container;

        public WeaponViewFactory(IWeaponShootingData shootingData, DiContainer container)
        {
            _shootingData = shootingData;
            _container = container;
        }

        public WeaponView Create(WeaponConfig config, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<WeaponView>(config.View, Vector3.zero, Quaternion.identity, container);
            var weapon = new Weapon(_shootingData.Shootings[config.ShootingType], config.Bullet);
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;
            view.transform.localScale = Vector3.one;
            view.Initialize(weapon);
            
            return view;
        }
    }
}
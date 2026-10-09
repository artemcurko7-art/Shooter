using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class WeaponViewFactory
    {
        private readonly WeaponShootingData _shootingData;
        private readonly DiContainer _container;

        public WeaponViewFactory(WeaponShootingData shootingData, DiContainer container)
        {
            _shootingData = shootingData;
            _container = container;
        }

        public WeaponView Create(WeaponConfig config, IAttacker attacker, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<WeaponView>(config.View, Vector3.zero, Quaternion.identity, container);
            var type = _shootingData.Shootings[config.ShootingType];
            var template = (IWeaponShooting)_container.Instantiate(type, new object[] { config });
            var weapon = new Weapon(template, attacker);
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;
            view.transform.localScale = config.Scale;
            view.Initialize(weapon);
            
            return view;
        }
    }
}
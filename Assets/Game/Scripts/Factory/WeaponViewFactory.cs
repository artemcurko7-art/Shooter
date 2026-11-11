using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Extensions;
using Game.Scripts.PoolMono.Repository.Projectile;
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
        private readonly IProjectilePoolRepository _projectilePoolRepository;
        private readonly DiContainer _container;

        public WeaponViewFactory(WeaponShootingData shootingData, IProjectilePoolRepository projectilePoolRepository, DiContainer container)
        {
            _shootingData = shootingData;
            _projectilePoolRepository = projectilePoolRepository;
            _container = container;
        }

        public WeaponView Create(WeaponConfig config, ICharacter character, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<WeaponView>(config.View, Vector3.zero, Quaternion.identity, container);
            var type = _shootingData.Types[config.ShootingType];
            var weaponShooting = _container.InstantiateWeaponShooting(type, config, _projectilePoolRepository.Pools[config.ProjectileType]);
            var weapon = new Weapon(weaponShooting, character);
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;
            view.transform.localScale = config.Scale;
            view.Initialize(weapon);
            
            return view;
        }
    }
}
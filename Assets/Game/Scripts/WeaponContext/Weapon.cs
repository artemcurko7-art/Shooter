using Game.Scripts.Provider;
using Game.Scripts.WeaponContext.Data;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Weapon
    {
        private readonly IWeaponShooting _shooting;
        private readonly Bullet _bullet;

        public Weapon(IWeaponShooting shooting, Bullet bullet)
        {
            _shooting = shooting;
            _bullet = bullet;
        }
        
        public void Shoot(Transform transform, Bullet bullet)
        {
            _shooting.Shoot();
            
            // var obj = GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            // obj.SetDirection(transform.forward);
            
            //_data.Shootings[_data.Weapons[_provider.Type][0].ShootingType].Shoot();
        }
    }
}
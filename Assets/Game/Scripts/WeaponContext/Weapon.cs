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
        
        public void Shoot(Transform transform)
        {
            _shooting.Shoot(transform, _bullet);
        }
    }
}
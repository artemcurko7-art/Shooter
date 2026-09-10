using Game.Scripts.Provider;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Weapon
    {
        public Weapon(IWeaponShooting shooting, Bullet bullet, Transform transform, float radius, int damage, float speed)
        {
            Shooting = shooting;
            
            shooting.StartShooting(bullet, transform, radius, damage, speed);
        }
        
        public IWeaponShooting Shooting { get; private set; }
    }
}
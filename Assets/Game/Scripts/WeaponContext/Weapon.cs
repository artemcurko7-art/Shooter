using Game.Scripts.Provider;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Weapon
    {
        public Weapon(IWeaponShooting shooting, Transform transform)
        {
            Shooting = shooting;
            
            shooting.StartShooting(transform);
        }
        
        public IWeaponShooting Shooting { get; private set; }
    }
}
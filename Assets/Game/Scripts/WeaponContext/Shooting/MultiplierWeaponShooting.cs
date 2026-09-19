using System;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class MultiplierWeaponShooting : WeaponShooting, IWeaponShooting
    {
        public event Action Attacked;
        
        public MultiplierWeaponShooting()
        {
            Type = ShootingType.Multiplier;
        }
        
        public ShootingType Type { get; }
        
        public void Subscribe()
        {
            
        }

        public void Unsubscribe()
        {
            
        }
        
        public void StartShooting(Bullet bullet, Transform transform, float radius, int damage, float speed)
        {
            Debug.Log("Multiplier");
        }
    }
}

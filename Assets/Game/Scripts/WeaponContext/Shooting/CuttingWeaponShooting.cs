using System;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class CuttingWeaponShooting : IWeaponShooting
    {
        public event Action Attacked;
        
        public CuttingWeaponShooting()
        {
            Type = ShootingType.Cutting;
        }
        
        public ShootingType Type { get; }

        public void Subscribe()
        {
            
        }

        public void Unsubscribe()
        {
            
        }
        
        public void StartShooting(Bullet bullet, float radius, int damage, float speed)
        {
            Debug.Log("Cutting");
        }
    }
}

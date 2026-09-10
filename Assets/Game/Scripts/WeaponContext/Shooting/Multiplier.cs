using System;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Multiplier : WeaponShooting, IWeaponShooting
    {
        public event Action Attacked;
        
        public Multiplier()
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
        
        public void StartShooting(Transform transform, Bullet bullet)
        {
            Debug.Log($"Multiplier");
        }
    }
}

using System;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Cutting : IWeaponShooting
    {
        public event Action Attacked;
        
        public Cutting()
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
        
        public void StartShooting(Bullet bullet, Transform transform, float radius, int damage, float speed)
        {
            Debug.Log("Cutting");
        }
    }
}

using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Cutting : IWeaponShooting
    {
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
        
        public void Shoot(Transform transform, Bullet bullet)
        {
            Debug.Log("Cutting");
        }
    }
}

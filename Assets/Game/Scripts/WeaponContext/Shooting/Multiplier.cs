using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Multiplier : IWeaponShooting
    {
        public Multiplier()
        {
            Type = ShootingType.Multiplier;
        }
        
        public ShootingType Type { get; }
        
        public void Shoot()
        {
            Debug.Log($"Multiplier");
        }
    }
}

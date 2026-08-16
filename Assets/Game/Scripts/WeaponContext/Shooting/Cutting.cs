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
        
        public void Shoot()
        {
            Debug.Log("Cutting");
        }
    }
}

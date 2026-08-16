using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Single : IWeaponShooting
    {
        public Single()
        {
            Type = ShootingType.Single;
        }
        
        public ShootingType Type { get; }
        
        public void Shoot()
        {
            Debug.Log("Single");
        }
    }
}
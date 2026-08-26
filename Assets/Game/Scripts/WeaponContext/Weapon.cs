using Game.Scripts.Provider;
using Game.Scripts.WeaponContext.Data;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Weapon
    {
        private readonly WeaponData _data;
        private readonly WeaponProvider _provider;
        
        // public Weapon(WeaponData data, WeaponProvider provider)
        // {
        //     _data = data;
        //     _provider = provider;
        // }
        
        public void Shoot(Transform transform, Bullet bullet)
        {
            // var obj = GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            // obj.SetDirection(transform.forward);
            
            //_data.Shootings[_data.Weapons[_provider.Type][0].ShootingType].Shoot();
        }
    }
}
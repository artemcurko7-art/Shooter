using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class WeaponView : MonoBehaviour
    {
        private Weapon _weapon;
    
        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
            
            _weapon.Shoot(null, null);
        }
    }
}
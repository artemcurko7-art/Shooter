using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class WeaponView : MonoBehaviour
    {
        private Weapon _weapon;
    
        public WeaponType Type { get; private set; }
    
        public void Initialize(WeaponType type, Weapon weapon)
        {
            Type = type;
            _weapon = weapon;
        }
    }
}
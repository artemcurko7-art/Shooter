using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField] public Transform LeftHandGrip { get; private set; }
        [field: SerializeField] public Transform RightHandGrip { get; private set; }
        
        private Weapon _weapon;

        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
            
            _weapon.Shoot(null, null);
        }
    }
}
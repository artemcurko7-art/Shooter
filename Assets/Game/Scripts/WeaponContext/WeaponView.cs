using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [field: SerializeField] public Transform LeftHandGrip { get; private set; }
        [field: SerializeField] public Transform RightHandGrip { get; private set; }
        
        public void Initialize(Weapon weapon)
        {
            weapon.Shoot(transform);
        }
    }
}
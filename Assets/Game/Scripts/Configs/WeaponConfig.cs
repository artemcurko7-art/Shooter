using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Weapon", fileName = "Weapon", order = 3)]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public ShootingType ShootingType { get; private set; }
        [field: SerializeField] public WeaponView View { get; private set; }
        [field: SerializeField] public Bullet Bullet { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
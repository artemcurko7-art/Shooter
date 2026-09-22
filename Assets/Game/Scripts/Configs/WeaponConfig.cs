using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Shooting;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Weapon", fileName = "Weapon", order = 4)]
    public class WeaponConfig : ScriptableObject, IWeaponShootingConfig
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public ShootingType ShootingType { get; private set; }
        [field: SerializeField] public WeaponView View { get; private set; }
        [field: SerializeField] public Projectile Projectile { get; private set; }
        [field: SerializeField] public int MaxCountShoot { get; private set; }
        [field: SerializeField] public float CooldownShoot { get; private set; }
        [field: SerializeField] public float CooldownReload { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
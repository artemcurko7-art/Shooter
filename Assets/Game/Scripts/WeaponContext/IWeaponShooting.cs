using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext
{
    public interface IWeaponShooting
    {
        ShootingType Type { get; }
        void Shoot();
    }
}
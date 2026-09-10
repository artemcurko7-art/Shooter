using Game.Scripts.Attacked;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public interface IWeaponShooting : IAttackable, ISubscriber
    {
        ShootingType Type { get; }
        void StartShooting(Transform transform, Bullet bullet);
    }
}
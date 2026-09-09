using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public interface IWeaponShooting : ISubscriber
    {
        ShootingType Type { get; }
        void Shoot(Transform transform, Bullet bullet);
    }
}
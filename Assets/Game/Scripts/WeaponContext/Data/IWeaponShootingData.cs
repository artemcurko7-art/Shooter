using System.Collections.Generic;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Data
{
    public interface IWeaponShootingData
    {
        public IReadOnlyDictionary<ShootingType, IWeaponShooting> Shootings { get; }
    }
}
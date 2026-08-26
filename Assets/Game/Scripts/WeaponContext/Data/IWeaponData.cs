using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Data
{
    public interface IWeaponData
    {
        public IReadOnlyDictionary<WeaponType, WeaponConfig> Weapons { get; }
    }
}
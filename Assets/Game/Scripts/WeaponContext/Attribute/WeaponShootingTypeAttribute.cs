using System;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.WeaponContext.Attribute
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class WeaponShootingTypeAttribute : System.Attribute
    {
        public WeaponShootingTypeAttribute(ShootingType type)
        {
            Type = type;
        }
        
        public ShootingType Type { get; }
    }
}
using Game.Scripts.Equipment.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Data;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.Equipment
{
    public class EquipmentItem
    {
        public EquipmentItem(Stat[] stats, EquipmentType type, WeaponType weaponType)
        {
            Stats = stats;
            Type = type;
            WeaponType = weaponType;
        }
        
        public EquipmentType Type { get; }
        public WeaponType WeaponType { get; }
        public Stat[] Stats { get; }
    }
}
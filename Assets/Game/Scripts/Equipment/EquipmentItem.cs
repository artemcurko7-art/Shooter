using Game.Scripts.Equipment.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.Equipment
{
    public class EquipmentItem
    {
        public EquipmentItem(Stat mainStat, Stat[] additionalStats, EquipmentType type, WeaponType weaponType)
        {
            MainStat = mainStat;
            AdditionalStats = additionalStats;
            Type = type;
            WeaponType = weaponType;
        }
        
        public EquipmentType Type { get; }
        public WeaponType WeaponType { get; }
        public Stat MainStat { get; }
        public Stat[] AdditionalStats { get; }
    }
}
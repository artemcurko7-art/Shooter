using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class EquipmentItem
    {
        public EquipmentItem(Stat mainStat, IReadOnlyDictionary<StatType, Stat> additionalStats, EquipmentType type, WeaponType weaponType)
        {
            MainStat = mainStat;
            AdditionalStats = additionalStats;
            Type = type;
            WeaponType = weaponType;
        }
        
        public EquipmentType Type { get; }
        public WeaponType WeaponType { get; }
        public Stat MainStat { get; }
        public IReadOnlyDictionary<StatType, Stat> AdditionalStats { get; }
    }
}
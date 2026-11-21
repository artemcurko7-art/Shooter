using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Type;

namespace Game.Scripts.Equipment.EquipmentContext.Data
{
    public interface IEquipmentData
    {
        public IReadOnlyDictionary<EquipmentType, List<EquipmentConfig>> Equipments { get; }
    }
}
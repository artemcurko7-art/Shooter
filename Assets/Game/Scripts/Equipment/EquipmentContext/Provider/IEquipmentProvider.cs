using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.Type;

namespace Game.Scripts.Equipment.EquipmentContext.Provider
{
    public interface IEquipmentProvider
    {
        public IReadOnlyDictionary<EquipmentType, EquipmentItem> EquipmentItems { get; }
    }
}
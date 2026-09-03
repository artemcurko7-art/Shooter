using System.Collections.Generic;
using Game.Scripts.Equipment.Type;

namespace Game.Scripts.Equipment.Repository
{
    public interface IEquipmentFreeSlotRegistry
    {
        public IReadOnlyDictionary<EquipmentType, Slot> EquippedSlots { get; }
    }
}
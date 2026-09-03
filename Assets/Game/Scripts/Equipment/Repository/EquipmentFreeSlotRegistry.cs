using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.Type;

namespace Game.Scripts.Equipment.Repository
{
    public class EquipmentFreeSlotRegistry : IEquipmentFreeSlotRegistry
    {
        private readonly Dictionary<EquipmentType, Slot> _equippedSlots = new();

        public EquipmentFreeSlotRegistry()
        {
            foreach (var type in Enum.GetValues(typeof(EquipmentType)))
            {
                if ((EquipmentType)type == EquipmentType.None)
                    continue;
                
                _equippedSlots.Add((EquipmentType)type, null);
            }
        }
        
        public IReadOnlyDictionary<EquipmentType, Slot> EquippedSlots => _equippedSlots;

        public void Register(EquipmentType equipmentType, Slot slot)
        {
            _equippedSlots[equipmentType] = slot;
        }

        public void Unregister(EquipmentType equipmentType)
        {
            _equippedSlots[equipmentType] = null;
        }

        public bool HasValue(Slot slot)
        {
            return _equippedSlots.ContainsValue(slot);
        }
    }
}
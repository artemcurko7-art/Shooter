using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.Repository;

namespace Game.Scripts.Equipment.Repository
{
    public class FreeSlotRegistry<TType, TSlot> : IFreeSlotRegistry<TType, TSlot> where TSlot : Slot
    {
        private readonly Dictionary<TType, TSlot> _equippedSlots = new();
        
        public FreeSlotRegistry()
        {
            foreach (var type in Enum.GetValues(typeof(TType)))
            {
                _equippedSlots.Add((TType)type, null);
            }
        }
        
        public IReadOnlyDictionary<TType, TSlot> EquippedSlots => _equippedSlots;
        
        public void Register(TType type, TSlot slot)
        {
            _equippedSlots[type] = slot;
        }
        
        public void Unregister(TType type)
        {
            _equippedSlots[type] = null;
        }
        
        public bool HasValue(TSlot slot)
        {
            return _equippedSlots.ContainsValue(slot);
        }
    }
}
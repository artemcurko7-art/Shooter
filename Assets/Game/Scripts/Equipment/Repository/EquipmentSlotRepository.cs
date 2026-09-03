using System.Collections.Generic;

namespace Game.Scripts.Equipment.Repository
{
    public class EquipmentSlotRepository
    {
        private readonly List<Slot> _slots = new();
        
        public IReadOnlyList<Slot> Slots => _slots;

        public void Add(Slot slot)
        {
            _slots.Add(slot);
        }

        public void Remove(Slot slot)
        {
            _slots.Remove(slot);
        }

        public bool Has(Slot slot)
        {
            return _slots.Contains(slot);
        }
    }
}
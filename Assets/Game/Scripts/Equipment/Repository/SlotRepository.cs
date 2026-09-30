using System.Collections.Generic;

namespace Game.Scripts.Equipment.Repository
{
    public class SlotRepository<T> where T : Slot
    {
        private readonly List<T> _slots = new();
        
        public IReadOnlyList<T> Slots => _slots;

        public void Add(T slot)
        {
            _slots.Add(slot);
        }

        public void Remove(T slot)
        {
            _slots.Remove(slot);
        }

        public bool Has(T slot)
        {
            return _slots.Contains(slot);
        } 
    }
}
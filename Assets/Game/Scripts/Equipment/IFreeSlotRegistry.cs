using System.Collections.Generic;

namespace Game.Scripts.Equipment
{
    public interface IFreeSlotRegistry<TType, TSlot>
    {
        public IReadOnlyDictionary<TType, TSlot> EquippedSlots { get; }
    }
}
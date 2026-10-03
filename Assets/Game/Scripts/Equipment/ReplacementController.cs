using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;

namespace Game.Scripts.Equipment
{
    public abstract class ReplacementController<TSelf, TType, TSlot> : SlotProcessor<TType, TSlot>, IReplaceable<TSelf>
        where TSelf : ReplacementController<TSelf, TType, TSlot>
        where TSlot : Slot
    {
        public ReplacementController(ISlotRewardService<TSlot> service,
            SlotRepository<TSlot> repository,
            DropSlot<TSlot>[] dropSlots,
            FreeSlotRegistry<TType, TSlot> freeRegistry)
            : base(service, repository, dropSlots, freeRegistry) { }

        public abstract void Replace();
    }
}
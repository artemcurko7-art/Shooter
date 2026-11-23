using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Observer;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.Static.Extensions;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class SlotProcessor<TType, TSlot> : SlotProcessorObserver<TSlot> where TSlot : Slot
    {
        protected SlotProcessor(
            ISlotRewardService<TSlot> service,
            SlotsRepository<TSlot> repository,
            DropSlot<TSlot>[] dropSlots,
            FreeSlotRegistry<TType, TSlot> freeRegistry)
            : base(service, repository, dropSlots)
        {
            FreeRegistry = freeRegistry;
        }

        public TSlot DraggedSlot { get; private set; }
        public TSlot DroppedSlot { get; private set; }
        protected FreeSlotRegistry<TType, TSlot> FreeRegistry { get; }

        protected override void OnBeginDragged(TSlot slot)
        {
            base.OnBeginDragged(slot);
            
            DraggedSlot = slot;
        }
        
        protected override void OnDropped(TSlot slot)
        {
            base.OnDropped(slot);
            
            DroppedSlot = slot;
        }
        
        protected void Release()
        {
            DroppedSlot = null;
        }

        protected void Assign(TSlot slot)
        {
            DroppedSlot = slot;
        }
    }
}
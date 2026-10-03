using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Observer;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Extensions;
using Game.Scripts.Service.Equipment.Reward;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class SlotProcessor<TType, TSlot> : SlotProcessorObserver<TSlot> where TSlot : Slot
    {
        protected SlotProcessor(
            ISlotRewardService<TSlot> service,
            SlotRepository<TSlot> repository,
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

        protected override void OnEndDragged(TSlot slot)
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Slot == null)
                {
                    dropSlot.Icon.color = Color.white;
                    dropSlot.Icon.color = dropSlot.Icon.color.GetAlpha(0f);
                }
            }
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
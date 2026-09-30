using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class SlotProcessor<TType, TSlot, TDropSlot> : ISubscriber 
        where TSlot : Slot 
        where TDropSlot : DropSlot<TSlot>
    {
        private readonly CharacterSlotRewardService _service;

        public SlotProcessor(SlotRepository<TSlot> repository, FreeSlotRegistry<TType, TSlot> freeRegistry, TDropSlot[] dropSlots)
        {
            DropSlots = dropSlots;
            //Sorting = sorting;
            Repository = repository;
            FreeRegistry = freeRegistry;
        }

        public TSlot DraggedSlot { get; private set; }
        public TSlot DroppedSlot { get; private set; }
        protected SlotRepository<TSlot> Repository { get; }
        protected FreeSlotRegistry<TType, TSlot> FreeRegistry { get; }
        //protected SortingEquipmentByParameters Sorting { get; }
        protected TDropSlot[] DropSlots { get; }

        public virtual void Subscribe()
        {
            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped += OnDropped;
        }

        public virtual void Unsubscribe()
        {
            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped -= OnDropped;
        }

        protected virtual void OnBeginDragged(TSlot slot)
        {
            DraggedSlot = slot;
        }

        protected virtual void OnEndDragged(TSlot slot) { }
        
        protected virtual void OnDropped(TSlot slot)
        {
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
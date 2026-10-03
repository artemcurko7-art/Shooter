using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment.Observer
{
    public abstract class SlotProcessorObserver<TSlot> : ISubscriber where TSlot : Slot
    {
        private readonly ISlotRewardService<TSlot> _service;
        
        protected SlotProcessorObserver(
            ISlotRewardService<TSlot> service,
            SlotRepository<TSlot> repository,
            DropSlot<TSlot>[] dropSlots)
        {
            _service = service;
            Repository = repository;
            DropSlots = dropSlots;
        }

        protected SlotRepository<TSlot> Repository { get; }
        protected DropSlot<TSlot>[] DropSlots { get; }

        public virtual void Subscribe()
        {
            _service.Rewarded += OnRewarded;
            
            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped += HandleDropped;
        }

        public virtual void Unsubscribe()
        {
            _service.Rewarded -= OnRewarded;
            
            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped -= HandleDropped;
            
            foreach (var slot in Repository.Slots)
            {
                slot.Drag.BeginDragged -= HandleBeginDragged;
                slot.Drag.EndDragged -= HandleEndDragged;
            }
        }

        protected virtual void OnBeginDragged(TSlot slot) { }

        protected virtual void OnEndDragged(TSlot slot) { }
        
        protected virtual void OnDropped(TSlot slot) { }
        
        protected virtual void OnRewarded(TSlot slot)
        {
            slot.Drag.BeginDragged -= HandleBeginDragged;
            slot.Drag.BeginDragged += HandleBeginDragged;
            slot.Drag.EndDragged -= HandleEndDragged;
            slot.Drag.EndDragged += HandleEndDragged;
            
            if (Repository.Has(slot))
                return;
            
            Repository.Add(slot);
        }

        private void HandleBeginDragged(Slot slot)
        {
            if (slot is TSlot typedSlot)
                OnBeginDragged(typedSlot);
        }
        
        private void HandleEndDragged(Slot slot)
        {
            if (slot is TSlot typedSlot)
                OnEndDragged(typedSlot);
        }

        private void HandleDropped(Slot slot)
        {
            if (slot is TSlot typedSlot)
                OnDropped(typedSlot);
        }
    }
}
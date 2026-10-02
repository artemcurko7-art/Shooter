using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.Observer
{
    public abstract class SlotProcessorObserver<TData, TFactory, TSlot, TDropSlot> : ISubscriber
        where TSlot : Slot
        where TDropSlot : Slot
    {
        private readonly SlotRewardService<TSlot, TData, TFactory> _service;
        private readonly SlotRepository<TSlot> _repository;
        private readonly TDropSlot[] _dropSlots;
        
        protected SlotProcessorObserver(
            SlotRewardService<TSlot, TData, TFactory> service,
            SlotRepository<TSlot> repository,
            TDropSlot[] dropSlots)
        {
            _service = service;
            _repository = repository;
            _dropSlots = dropSlots;
        }

        public virtual void Subscribe()
        {
            _service.Added += OnAdded;
        }

        public virtual void Unsubscribe()
        {
            _service.Added -= OnAdded;

            foreach (var slot in _repository.Slots)
            {
                slot.Drag.BeginDragged -= OnBeginDragged;
                slot.Drag.EndDragged -= OnEndDragged;
            }
        }

        protected virtual void OnBeginDragged(Slot slot) { }

        protected virtual void OnEndDragged(Slot slot) { }
        
        protected virtual void OnDropped(Slot slot) { }
        
        private void OnAdded(Slot slot)
        {
            slot.Drag.BeginDragged -= OnBeginDragged;
            slot.Drag.BeginDragged += OnBeginDragged;
            slot.Drag.EndDragged -= OnEndDragged;
            slot.Drag.EndDragged += OnEndDragged;
        }
    }
}
using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.Handler
{
    public class SlotHandler : ITabService, ISubscriber
    {
        private readonly DropSlot[] _dropSlots;
        private readonly SortingEquipmentByParameters _sorting;
        private readonly List<DropSlot> _busyDropSlots = new();
        private readonly List<Slot> _slots = new(); 
        private Slot _droppedSlot;
        
        public event Action<bool> TabOpened;
        
        public SlotHandler(DropSlot[] dropSlots, SortingEquipmentByParameters sorting)
        {
            _dropSlots = dropSlots;
            _sorting = sorting;
        }

        public IReadOnlyList<Slot> Slots => _slots;
        
        public void Subscribe()
        {
            foreach (var slot in _slots)
            {
                slot.Drag.BeginDragged += OnBeginDragged;
                slot.Drag.EndDragged += OnEndDragged;
            }
        
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped += OnDropped;
        }
        
        public void Unsubscribe()
        {
            foreach (var slot in _slots)
            {
                slot.Drag.BeginDragged -= OnBeginDragged;
                slot.Drag.EndDragged -= OnEndDragged;
            }
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped -= OnDropped;
        }

        public void Add(Slot slot)
        {
            _slots.Add(slot);
            _sorting.Sort(_slots);
        }
        
        public void DisableTab()
        {
            TabOpened?.Invoke(false);
        }
        
        private void OnBeginDragged(Slot slot)
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.Slot == slot)
                {
                    dropSlot.Clear();
                    _busyDropSlots.Remove(dropSlot);
                    _droppedSlot = null;
                }
            }
        }
        
        private void OnEndDragged(Slot slot)
        {
            if (_slots.Contains(slot) == false && slot != _droppedSlot)
            {
                _slots.Add(slot); 
                _sorting.Sort(_slots);
            }
            
            slot.Drag.CanvasGroup.blocksRaycasts = true;
        }
        
        private void OnDropped(Slot slot)
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.Slot == slot)
                {
                    if (_busyDropSlots.Contains(dropSlot))
                        TabOpened?.Invoke(true);
                    else
                        _busyDropSlots.Add(dropSlot);
                }
            }
            
            _slots.Remove(slot);
            _sorting.Sort(_slots);
            _droppedSlot = slot;
        }
    }
}
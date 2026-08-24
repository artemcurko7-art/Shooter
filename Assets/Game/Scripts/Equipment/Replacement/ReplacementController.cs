using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementController : ISubscriber
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ITabService _tabService;
        private readonly DropSlot[] _dropSlots;
        private readonly Dictionary<EquipmentType, Slot> _busyDropSlots = new();
        private Slot _draggedSlot;
        private Slot _droppedSlot;
        
        public ReplacementController(IEquipmentService equipmentService, ITabService tabService, DropSlot[] dropSlots)
        {
            _equipmentService = equipmentService;
            _tabService = tabService;
            _dropSlots = dropSlots;

            foreach (var dropSlot in dropSlots)
                _busyDropSlots.Add(dropSlot.EquipmentType, null);
        }

        public void Subscribe()
        {
            foreach (var slot in _equipmentService.Slots)
            {
                slot.Drag.BeginDragged += OnBeginDraggedSlot;
                slot.Drag.EndDragged += OnEndDraggedSlot;
            }

            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped += OnDroppedSlot;
        }

        public void Unsubscribe()
        {
            foreach (var slot in _equipmentService.Slots)
            {
                slot.Drag.BeginDragged -= OnBeginDraggedSlot;
                slot.Drag.EndDragged -= OnEndDraggedSlot;
            }
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped -= OnDroppedSlot;
        }

        public void Replace()
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.EquipmentType == _droppedSlot.EquipmentType)
                {
                    dropSlot.SetReplacement(_busyDropSlots[dropSlot.EquipmentType], _droppedSlot);
                    _tabService.DisableTab();
                    _equipmentService.Sort();
                    _busyDropSlots[dropSlot.EquipmentType] = _droppedSlot;
                }
            }
        }

        private void OnBeginDraggedSlot(Slot slot)
        {
            if (_busyDropSlots[slot.EquipmentType] == slot)
                _busyDropSlots[slot.EquipmentType] = null;
        }
        
        private void OnEndDraggedSlot(Slot slot)
        {
            //_draggedSlot = slot;
        }

        private void OnDroppedSlot(Slot slot)
        {
            if (_busyDropSlots[slot.EquipmentType] == null)
                _busyDropSlots[slot.EquipmentType] = slot;
            else
                _droppedSlot = slot;
        }
    }
}
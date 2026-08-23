using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementController : ISubscriber
    {
        private readonly IEquipmentService _equipmentService;
        private readonly DropSlot[] _dropSlots;
        private Slot _draggedSlot;
        private Slot _droppedSlot;
        
        public ReplacementController(IEquipmentService equipmentService, DropSlot[] dropSlots)
        {
            _equipmentService = equipmentService;
            _dropSlots = dropSlots;
        }

        public void Subscribe()
        {
            foreach (var slot in _equipmentService.Slots)
                slot.Drag.EndDragged += OnEndDraggedSlot;

            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped += OnDroppedSlot;
        }

        public void Unsubscribe()
        {
            foreach (var slot in _equipmentService.Slots)
                slot.Drag.EndDragged -= OnEndDraggedSlot;
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped -= OnDroppedSlot;
        }

        public void Replace()
        {
            foreach (var slot in _dropSlots)
            {
                if (slot.EquipmentType == _draggedSlot.EquipmentType)
                {
                    slot.Set(_draggedSlot);
                    slot.DisableTab();
                }
            }
        }
        
        private void OnEndDraggedSlot(Slot slot)
        {
            _draggedSlot = slot;
        }

        private void OnDroppedSlot(Slot slot)
        {
            _droppedSlot = slot;
        }
    }
}
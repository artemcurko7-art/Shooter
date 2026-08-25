using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementController : ISubscriber
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ITabService _tabService;
        private readonly DropSlot[] _dropSlots;
        private readonly Dictionary<EquipmentType, Slot> _busyDropSlots = new();
        private readonly GridLayoutGroup _gridLayoutGroup;
        private Slot _droppedSlot;
        private bool _isTabActive;
        
        public ReplacementController(IEquipmentService equipmentService, ITabService tabService, DropSlot[] dropSlots, GridLayoutGroup gridLayoutGroup)
        {
            _equipmentService = equipmentService;
            _tabService = tabService;
            _dropSlots = dropSlots;
            _gridLayoutGroup = gridLayoutGroup;

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

            _tabService.TabOpened += OnTabOpened;
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
            
            _tabService.TabOpened -= OnTabOpened;
        }

        public void Replace()
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.EquipmentType == _droppedSlot.EquipmentType)
                {
                    dropSlot.Set(_droppedSlot);
                    _tabService.DisableTab();
                    _busyDropSlots[dropSlot.EquipmentType].Drag.ResetSettings();
                    _busyDropSlots[dropSlot.EquipmentType] = _droppedSlot;
                    _gridLayoutGroup.enabled = true;
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
            if (_isTabActive == false)
                _gridLayoutGroup.enabled = true;
        }
        
        private void OnDroppedSlot(Slot slot)
        {
            if (_busyDropSlots[slot.EquipmentType] == null)
                _busyDropSlots[slot.EquipmentType] = slot;
            else
                _droppedSlot = slot;
        }

        private void OnTabOpened(bool isActive)
        {
            _isTabActive = isActive;
        }
    }
}
using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Replacement;
using Game.Scripts.Factory;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class ReplacementService : ISubscriber
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ITabService _tabService;
        private readonly DisplayStatData _displayStatData;
        private readonly DropSlot[] _dropSlots;
        private readonly DisplayStatFactory _displayStatFactory;
        private readonly List<DisplayStat> _displayStats = new();
        private readonly Transform _container;
        private Slot _draggedSlot;
        private Slot _droppedSlot;
        
        public ReplacementService(
            IEquipmentService equipmentService, 
            ITabService tabService, 
            DisplayStatData displayStatData,
            DisplayStatFactory displayStatFactory,
            DropSlot[] dropSlots, 
            Transform container)
        {
            _tabService = tabService;
            _equipmentService = equipmentService;
            _displayStatData = displayStatData;
            _displayStatFactory = displayStatFactory;
            _dropSlots = dropSlots;
            _container = container;
        }
        
        public void Subscribe()
        {
            _equipmentService.Added += OnAdded;
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped += OnDropped;
            
            _tabService.TabOpened += OnTabOpened;
        }

        public void Unsubscribe()
        {
            _equipmentService.Added -= OnAdded;
            
            foreach (var slot in _equipmentService.Slots)
                slot.Drag.BeginDragged -= OnBeginDragged;
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped -= OnDropped;
            
            _tabService.TabOpened -= OnTabOpened;
        }

        private void OnAdded(Slot slot)
        {
            slot.Drag.BeginDragged += OnBeginDragged;
        }
        
        private void OnBeginDragged(Slot slot)
        {
            if (_draggedSlot == _droppedSlot)
                _droppedSlot = null;
            
            _draggedSlot = slot;
        }

        private void OnDropped(Slot slot)
        {
            _droppedSlot = slot;
        }

        private void OnTabOpened(bool isActive)
        {
            if (isActive)
            {
                foreach (var stat in _draggedSlot.EquipmentItem.AdditionalStats)
                {
                    _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], _container,
                        (int)stat.Value, stat.IsPercentageValue));
                }
            }
            else
            {
                foreach (var stat in _displayStats)
                {
                    stat.OnDisabled();
                }
                
                _displayStats.Clear();
            }
        }
    }
}
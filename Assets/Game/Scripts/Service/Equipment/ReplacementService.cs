using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Replacement;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
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
        private readonly Dictionary<EquipmentType, Slot> _busyDropSlots = new();
        private readonly List<DisplayStat> _displayStats = new();
        private readonly Transform[] _containers;
        private Slot _draggedSlot;
        private Slot _droppedSlot;
        private Slot _oldDroppedSlot;
        
        public ReplacementService(
            IEquipmentService equipmentService, 
            ITabService tabService, 
            DisplayStatData displayStatData,
            DisplayStatFactory displayStatFactory,
            DropSlot[] dropSlots, 
            Transform[] containers)
        {
            _tabService = tabService;
            _equipmentService = equipmentService;
            _displayStatData = displayStatData;
            _displayStatFactory = displayStatFactory;
            _dropSlots = dropSlots;
            _containers = containers;

            foreach (var dropSlot in dropSlots)
                _busyDropSlots.Add(dropSlot.EquipmentType, null);
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
            if (_busyDropSlots[slot.EquipmentItem.Type] == slot)
            {
                _busyDropSlots[slot.EquipmentItem.Type] = null;
                _droppedSlot = null;
            }
            
            _draggedSlot = slot;
        }

        private void OnDropped(Slot slot)
        {
            if (_busyDropSlots[slot.EquipmentItem.Type] == null)
                _busyDropSlots[slot.EquipmentItem.Type] = slot;
            
            _droppedSlot = slot;
        }

        private void OnTabOpened(bool isActive)
        {
            if (isActive)
            {
                Stat mainDraggedStat = _draggedSlot.EquipmentItem.MainStat;
                
                _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[mainDraggedStat.Type], _containers[2],
                    (int)mainDraggedStat.Value, mainDraggedStat.IsPercentageValue));
                
                foreach (var stat in _draggedSlot.EquipmentItem.AdditionalStats)
                {
                    _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], _containers[3],
                        (int)stat.Value, stat.IsPercentageValue));
                }
                
                Stat mainDroppedStat = _busyDropSlots[_droppedSlot.EquipmentItem.Type].EquipmentItem.MainStat;
                
                _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[mainDroppedStat.Type], _containers[0],
                    (int)mainDroppedStat.Value, mainDroppedStat.IsPercentageValue));
                
                foreach (var stat in _busyDropSlots[_droppedSlot.EquipmentItem.Type].EquipmentItem.AdditionalStats)
                {
                    _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], _containers[1],
                        (int)stat.Value, stat.IsPercentageValue));
                }
                
                // Stat mainDraggedStat = _draggedSlot.EquipmentItem.MainStat;
                //
                // _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[mainDraggedStat.Type], _containers[2],
                //     (int)mainDraggedStat.Value, mainDraggedStat.IsPercentageValue));
                //
                // foreach (var stat in _draggedSlot.EquipmentItem.AdditionalStats)
                // {
                //     _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], _containers[3],
                //         (int)stat.Value, stat.IsPercentageValue));
                // }
                //
                // Stat mainDroppedStat = _droppedSlot.EquipmentItem.MainStat;
                //
                // _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[mainDroppedStat.Type], _containers[0],
                //     (int)mainDroppedStat.Value, mainDroppedStat.IsPercentageValue));
                //
                // foreach (var stat in _droppedSlot.EquipmentItem.AdditionalStats)
                // {
                //     _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], _containers[1],
                //         (int)stat.Value, stat.IsPercentageValue));
                // }
            }
            else
            {
                foreach (var stat in _displayStats)
                {
                    stat.OnDestroyed();
                }
                
                _displayStats.Clear();
            }
        }
    }
}
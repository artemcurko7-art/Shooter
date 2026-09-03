using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class SlotProcessor : ISubscriber
    {
        private readonly IEquipmentService _equipmentService;

        public SlotProcessor(
            IEquipmentService equipmentService,
            EquipmentSlotRepository repository,
            EquipmentFreeSlotRegistry freeRegistry,
            SortingEquipmentByParameters sorting,
            DropSlot[] dropSlots)
        {
            _equipmentService = equipmentService;
            DropSlots = dropSlots;
            Sorting = sorting;
            Repository = repository;
            FreeRegistry = freeRegistry;
        }

        protected EquipmentSlotRepository Repository { get; }
        protected EquipmentFreeSlotRegistry FreeRegistry { get; }
        protected SortingEquipmentByParameters Sorting { get; }
        protected DropSlot[] DropSlots { get; }
        protected Slot DraggedSlot { get; private set; }
        protected Slot DroppedSlot { get; private set; }

        public virtual void Subscribe()
        {
            _equipmentService.Added += OnAdded;

            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped += OnDropped;
        }

        public virtual void Unsubscribe()
        {
            _equipmentService.Added -= OnAdded;

            foreach (var slot in Repository.Slots)
            {
                slot.Drag.BeginDragged -= OnBeginDragged;
                slot.Drag.EndDragged -= OnEndDragged;
            }

            foreach (var dropSlot in DropSlots)
                dropSlot.Dropped -= OnDropped;
        }

        protected virtual void OnBeginDragged(Slot slot)
        {
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == slot)
                FreeRegistry.Unregister(slot.EquipmentItem.Type);

            DraggedSlot = slot;
        }

        protected virtual void OnEndDragged(Slot slot) { }
        
        protected virtual void OnDropped(Slot slot)
        {
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == null)
                FreeRegistry.Register(slot.EquipmentItem.Type, slot);
            
            DroppedSlot = slot;
        }

        protected void Release()
        {
            DroppedSlot = null;
        }

        protected void Assign(Slot slot)
        {
            DroppedSlot = slot;
        }

        private void OnAdded(Slot slot)
        {
            slot.Drag.BeginDragged -= OnBeginDragged;
            slot.Drag.BeginDragged += OnBeginDragged;
            slot.Drag.EndDragged -= OnEndDragged;
            slot.Drag.EndDragged += OnEndDragged;

            if (Repository.Has(slot))
                return;
            
            Repository.Add(slot);
            Sorting.Sort(Repository.Slots);
        }
    }
}
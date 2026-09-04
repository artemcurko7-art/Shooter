using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment;
using UnityEngine;

namespace Game.Scripts.Equipment.Handler
{
    public class SlotHandler : SlotProcessor, ITabService
    {
        private readonly List<DropSlot> _equippedSlots = new();
        
        public event Action<bool> TabOpened;
        
        public SlotHandler(
            IEquipmentService equipmentService,
            EquipmentSlotRepository repository,
            EquipmentFreeSlotRegistry freeRegistry,
            SortingEquipmentByParameters sorting,
            DropSlot[] dropSlots) :
            base(equipmentService, repository, freeRegistry, sorting, dropSlots) { }

        public void DisableTab()
        {
            TabOpened?.Invoke(false);
        }

        protected override void OnBeginDragged(Slot slot)
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Slot == slot)
                {
                    dropSlot.Clear();
                    _equippedSlots.Remove(dropSlot);
                    Release();
                }
            }
        }
        
        protected override void OnEndDragged(Slot slot)
        {
            if (Repository.Has(slot) == false && slot != DroppedSlot)
            {
                Repository.Add(slot);
                Sorting.Sort(Repository.Slots);
            }
            
            slot.Drag.CanvasGroup.blocksRaycasts = true;
        }
        
        protected override void OnDropped(Slot slot)
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Slot == slot)
                {
                    if (_equippedSlots.Contains(dropSlot))
                        TabOpened?.Invoke(true);
                    else
                        _equippedSlots.Add(dropSlot);
                }
            }

            Repository.Remove(slot);
            Sorting.Sort(Repository.Slots);
            Assign(slot);
        }
    }
}
using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.EquipmentContext;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext.Handler
{
    public class EquipmentSlotHandler : EquipmentSlotProcessor, ITabService<EquipmentSlotHandler>
    {
        private readonly List<EquipmentDropSlot> _equippedSlots = new();

        public EquipmentSlotHandler(
            SlotRepository<EquipmentSlot> repository,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            EquipmentDropSlot[] dropSlots,
            IEquipmentService equipmentService,
            SortingEquipmentByParameters sorting) 
            : base(equipmentService, repository, freeRegistry, dropSlots, sorting) { }

        public event Action<bool> TabOpened;
        
        public void DisableTab()
        {
            TabOpened?.Invoke(false);
        }

        protected override void OnBeginDragged(EquipmentSlot slot)
        {
            base.OnBeginDragged(slot);
            
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
        
        protected override void OnEndDragged(EquipmentSlot slot)
        {
            if (Repository.Has(slot) == false && slot != DroppedSlot)
            {
                Repository.Add(slot);
                Sorting.Sort(Repository.Slots);
            }
            
            slot.Drag.CanvasGroup.blocksRaycasts = true;
        }
        
        protected override void OnDropped(EquipmentSlot slot)
        {
            base.OnDropped(slot);

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
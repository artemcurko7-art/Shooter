using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.EquipmentContext;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public abstract class EquipmentSlotProcessor : SlotProcessor<EquipmentType, EquipmentSlot, EquipmentDropSlot>
    {
        private readonly IEquipmentService _equipmentService;
        
        protected EquipmentSlotProcessor(
            IEquipmentService equipmentService,
            SlotRepository<EquipmentSlot> repository,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            EquipmentDropSlot[] dropSlots,
            SortingEquipmentByParameters sorting)
            : base(repository, freeRegistry, dropSlots)
        {
            _equipmentService = equipmentService;
            Sorting = sorting;
        }
        
        protected SortingEquipmentByParameters Sorting { get; }

        public override void Subscribe()
        {
            base.Subscribe();
            
            _equipmentService.Added += OnAdded;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            
            _equipmentService.Added -= OnAdded;

            // foreach (var slot in Repository.Slots)
            // {
            //     slot.Drag.BeginDragged -= OnBeginDragged;
            //     slot.Drag.EndDragged -= OnEndDragged;
            // }
        }

        protected override void OnBeginDragged(EquipmentSlot slot)
        {
            base.OnBeginDragged(slot);
            
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == slot)
                FreeRegistry.Unregister(slot.EquipmentItem.Type);
        }
        
        protected override void OnDropped(EquipmentSlot slot)
        {
            base.OnDropped(slot);
            
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == null)
                FreeRegistry.Register(slot.EquipmentItem.Type, slot);
        }
        
        private void OnAdded(EquipmentSlot slot)
        {
            // slot.Drag.BeginDragged -= OnBeginDragged;
            // slot.Drag.BeginDragged += OnBeginDragged;
            // slot.Drag.EndDragged -= OnEndDragged;
            // slot.Drag.EndDragged += OnEndDragged;

            if (Repository.Has(slot))
                return;
            
            Repository.Add(slot);
            Sorting.Sort(Repository.Slots);
        }
    }
}
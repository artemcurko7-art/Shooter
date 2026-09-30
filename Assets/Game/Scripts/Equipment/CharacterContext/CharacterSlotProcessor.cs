using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.CharacterContext;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public abstract class CharacterSlotProcessor : SlotProcessor<CharacterPlaceDropSlotType, CharacterSlot, CharacterDropSlot>
    {
        private readonly CharacterSlotRewardService _service;
        private CharacterPlaceDropSlotType _type;
        
        protected CharacterSlotProcessor(
            SlotRepository<CharacterSlot> repository,
            FreeSlotRegistry<CharacterPlaceDropSlotType, CharacterSlot> freeRegistry,
            CharacterDropSlot[] dropSlots,
            CharacterSlotRewardService service)
            : base(repository, freeRegistry, dropSlots)
        {
            _service = service;
        }
        
        //protected SortingEquipmentByParameters Sorting { get; }

        public override void Subscribe()
        {
            base.Subscribe();
            
            _service.Added += OnAdded;
            
            foreach (var dropSlot in DropSlots)
                dropSlot.TypeDropped += OnTypeDropped;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            
            _service.Added -= OnAdded;

            foreach (var slot in Repository.Slots)
            {
                slot.Drag.BeginDragged -= OnBeginDragged;
                slot.Drag.EndDragged -= OnEndDragged;
            }
            
            foreach (var dropSlot in DropSlots)
                dropSlot.TypeDropped -= OnTypeDropped;
        }

        protected override void OnBeginDragged(CharacterSlot slot)
        {
            base.OnBeginDragged(slot);
            
            foreach (var dropSlot in DropSlots)
                if (FreeRegistry.EquippedSlots[dropSlot.Type] == slot)
                    FreeRegistry.Unregister(dropSlot.Type);
        }
        
        protected override void OnDropped(CharacterSlot slot)
        {
            base.OnDropped(slot);

            if (FreeRegistry.EquippedSlots[_type] == null)
                FreeRegistry.Register(_type, slot);
        }
        
        private void OnAdded(CharacterSlot slot)
        {
            slot.Drag.BeginDragged -= OnBeginDragged;
            slot.Drag.BeginDragged += OnBeginDragged;
            slot.Drag.EndDragged -= OnEndDragged;
            slot.Drag.EndDragged += OnEndDragged;

            if (Repository.Has(slot))
                return;
            
            Repository.Add(slot);
            //Sorting.Sort(Repository.Slots);
        }

        private void OnTypeDropped(CharacterPlaceDropSlotType type)
        {
            _type = type;
        }
    }
}
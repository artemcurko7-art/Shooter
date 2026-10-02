using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Extensions;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public abstract class CharacterSlotProcessor : SlotProcessor<SquadNumberType, CharacterSlot, CharacterDropSlot>
    {
        private readonly CharacterSlotRewardService _service;
        private SquadNumberType _type;
        
        protected CharacterSlotProcessor(
            SlotRepository<CharacterSlot> repository,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry,
            CharacterDropSlot[] dropSlots,
            CharacterProvider provider,
            CharacterSlotRewardService service)
            : base(repository, freeRegistry, dropSlots)
        {
            Provider = provider;
            _service = service;
        }
        
        protected CharacterProvider Provider { get; private set; }
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
            
            // _service.Added -= OnAdded;
            //
            // foreach (var slot in Repository.Slots)
            // {
            //     slot.Drag.BeginDragged -= OnBeginDragged;
            //     slot.Drag.EndDragged -= OnEndDragged;
            // }
            //
            // foreach (var dropSlot in DropSlots)
            //     dropSlot.TypeDropped -= OnTypeDropped;
        }

        protected override void OnBeginDragged(CharacterSlot slot)
        {
            base.OnBeginDragged(slot);
            
            foreach (var dropSlot in DropSlots)
            {
                if (FreeRegistry.EquippedSlots[dropSlot.Type] == slot)
                {
                    FreeRegistry.Unregister(dropSlot.Type);
                    Provider.Remove(dropSlot.Type);
                }
                
                dropSlot.Icon.color = Color.green;
                dropSlot.Icon.color = dropSlot.Icon.color.GetAlpha( 0.5f);
            }
        }

        protected override void OnEndDragged(CharacterSlot slot)
        {
            // if (slot.Drag.IsDropSlotSuccess == false)
            // {
            //     foreach (var dropSlot in DropSlots)
            //     {
            //         dropSlot.Icon.color = Color.white;
            //         dropSlot.Icon.color = dropSlot.Icon.color.GetAlpha(0f);
            //     }
            // }
        }
        
        protected override void OnDropped(CharacterSlot slot)
        {
            base.OnDropped(slot);

            if (FreeRegistry.EquippedSlots[_type] == null)
            {
                FreeRegistry.Register(_type, slot);
                Provider.Set(_type, slot.Character);
            }
        }
        
        private void OnAdded(CharacterSlot slot)
        {
            // slot.Drag.BeginDragged -= OnBeginDragged;
            // slot.Drag.BeginDragged += OnBeginDragged;
            // slot.Drag.EndDragged -= OnEndDragged;
            // slot.Drag.EndDragged += OnEndDragged;

            if (Repository.Has(slot))
                return;
            
            Repository.Add(slot);
            //Sorting.Sort(Repository.Slots);
        }

        private void OnTypeDropped(SquadNumberType type)
        {
            _type = type;
        }
    }
}
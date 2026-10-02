using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.CharacterContext.Repository;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Handler
{
    public class CharacterSlotHandler : CharacterSlotProcessor, ITabService<CharacterSlotHandler>
    {
        private readonly List<DropSlot<CharacterSlot>> _equippedSlots = new();

        public CharacterSlotHandler(
            SlotRepository<CharacterSlot> repository,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry,
            CharacterDropSlot[] dropSlots,
            CharacterProvider provider,
            CharacterSlotRewardService service)
            : base(repository, freeRegistry, dropSlots, provider, service) { }
        
        public event Action<bool> TabOpened;

        public SquadNumberType DropSlotType { get; private set; }
        
        public void DisableTab()
        {
            TabOpened?.Invoke(false);
        }

        protected override void OnBeginDragged(CharacterSlot slot)
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
        
        protected override void OnEndDragged(CharacterSlot slot)
        {
            if (Repository.Has(slot) == false && slot != DroppedSlot)
            {
                Repository.Add(slot);
                //Sorting.Sort(Repository.Slots);
            }
            
            slot.Drag.CanvasGroup.blocksRaycasts = true;
        }
        
        protected override void OnDropped(CharacterSlot slot)
        {
            base.OnDropped(slot);
            
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Slot == slot)
                {
                    if (_equippedSlots.Contains(dropSlot))
                    {
                        DropSlotType = dropSlot.Type;
                        TabOpened?.Invoke(true);
                    }
                    else
                    {
                        _equippedSlots.Add(dropSlot);
                    }
                }
            }

            Repository.Remove(slot);
            //Sorting.Sort(Repository.Slots);
            Assign(slot);
        }
    }
}
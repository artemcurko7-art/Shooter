using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Extensions;
using Game.Scripts.Service.Equipment.Reward;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class SlotHandler<TSelf, TType, TSlot> : SlotProcessor<TType, TSlot>, ITabService<TSelf> 
        where TSelf : SlotHandler<TSelf, TType, TSlot>
        where TSlot : Slot
    {
        private readonly List<DropSlot<TSlot>> _equippedSlots = new();
        
        public SlotHandler(
            ISlotRewardService<TSlot> service,
            SlotRepository<TSlot> repository,
            DropSlot<TSlot>[] dropSlots,
            FreeSlotRegistry<TType, TSlot> freeRegistry)
            : base(service, repository, dropSlots, freeRegistry) { }

        public event Action<bool> TabOpened;
        
        public void DisableTab()
        {
            TabOpened?.Invoke(false);
        }
        
        protected override void OnBeginDragged(TSlot slot)
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

                if (dropSlot.Slot == null)
                {
                    dropSlot.Icon.color = Color.green;
                    dropSlot.Icon.color = dropSlot.Icon.color.GetAlpha( 0.5f);
                }
            }
        }
        
        protected override void OnEndDragged(TSlot slot)
        {
            base.OnEndDragged(slot);
            
            if (Repository.Has(slot) == false && slot != DroppedSlot)
            {
                Repository.Add(slot);
                //Sorting.Sort(Repository.Slots);
            }
            
            slot.Drag.CanvasGroup.blocksRaycasts = true;
        }
        
        protected override void OnDropped(TSlot slot)
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
            //Sorting.Sort(Repository.Slots);
            Assign(slot);
        }
    }
}
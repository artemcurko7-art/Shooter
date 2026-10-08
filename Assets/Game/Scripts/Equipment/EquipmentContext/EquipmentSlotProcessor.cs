using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Provider;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public abstract class EquipmentSlotProcessor : SlotProcessor<EquipmentType, EquipmentSlot>
    {
        private readonly EquipmentProvider _provider;
        private readonly SortingEquipmentByParameters _sorting;
        
        protected EquipmentSlotProcessor(
            ISlotRewardService<EquipmentSlot> service,
            SlotRepository<EquipmentSlot> repository,
            EquipmentProvider provider,
            EquipmentDropSlot[] dropSlots,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            SortingEquipmentByParameters sorting)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _provider = provider;
            _sorting = sorting;
        }
        
        protected override void OnBeginDragged(EquipmentSlot slot)
        {
            base.OnBeginDragged(slot);
                
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == slot)
            {
                FreeRegistry.Unregister(slot.EquipmentItem.Type);
                _provider.Remove(slot.EquipmentItem.Type);
            }
        }
        
        protected override void OnDropped(EquipmentSlot slot)
        {
            base.OnDropped(slot);
            
            if (FreeRegistry.EquippedSlots[slot.EquipmentItem.Type] == null)
            {
                FreeRegistry.Register(slot.EquipmentItem.Type, slot);
                _provider.Set(slot.EquipmentItem.Type, slot.EquipmentItem);
            }
        }
        
        protected override void OnRewarded(EquipmentSlot slot)
        {
            base.OnRewarded(slot);
            
            _sorting.Sort(Repository.Slots);
        }
    }
}
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;

namespace Game.Scripts.Equipment.EquipmentContext.Handler
{
    public class EquipmentSlotHandler : SlotHandler<EquipmentSlotHandler, EquipmentType, EquipmentSlot>
    {
        public EquipmentSlotHandler(
            ISlotRewardService<EquipmentSlot> service,
            SlotRepository<EquipmentSlot> repository,
            EquipmentDropSlot[] dropSlots,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry)
            : base(service, repository, dropSlots, freeRegistry) { }
    }
}
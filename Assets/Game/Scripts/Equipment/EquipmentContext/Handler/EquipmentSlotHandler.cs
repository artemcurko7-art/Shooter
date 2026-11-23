using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext.Handler
{
    public class EquipmentSlotHandler : SlotHandler<EquipmentSlotHandler, EquipmentType, EquipmentSlot>
    {
        private readonly DropZoneIndicator _dropZoneIndicator;
        private readonly EquipmentDropSlot[] _dropSlots;
        
        public EquipmentSlotHandler(
            DropZoneIndicator dropZoneIndicator,
            ISlotRewardService<EquipmentSlot> service,
            SlotsRepository<EquipmentSlot> repository,
            EquipmentDropSlot[] dropSlots,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _dropZoneIndicator = dropZoneIndicator;
            _dropSlots = dropSlots;
        }

        protected override void Select(EquipmentSlot slot)
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.Slot == null && dropSlot.EquipmentType == slot.EquipmentItem.Type)
                {
                    _dropZoneIndicator.gameObject.transform.SetParent(dropSlot.transform);
                    _dropZoneIndicator.transform.localPosition = Vector3.zero;
                    _dropZoneIndicator.gameObject.SetActive(true);
                }
            }
        }

        protected override void Unselect(EquipmentSlot slot)
        {
            _dropZoneIndicator.gameObject.SetActive(false);
        }
    }
}
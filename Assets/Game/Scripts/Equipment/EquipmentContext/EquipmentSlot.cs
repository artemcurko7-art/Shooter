using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class EquipmentSlot : Slot
    {
        [field: SerializeField] public EquipmentDragSlot EquipmentDrag;

        public EquipmentItem EquipmentItem { get; private set; }

        public void Initialize(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            GetDrag();
        }
        
        protected override DragSlot GetDrag()
        {
            EquipmentDrag.Initialize(this);
            return EquipmentDrag;
        }
    }
}
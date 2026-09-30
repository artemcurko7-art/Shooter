using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class EquipmentSlot : Slot
    {
        [field: SerializeField] public EquipmentDragSlot Drag { get; private set; }
        
        public EquipmentItem EquipmentItem { get; private set; }

        public void Initialize(EquipmentItem equipmentItem)
        {
            EquipmentItem = equipmentItem;
            Drag.Initialize(this);
        }
    }
}
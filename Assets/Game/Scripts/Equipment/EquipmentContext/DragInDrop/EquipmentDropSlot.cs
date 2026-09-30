using System;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.EquipmentContext.DragInDrop
{
    public class EquipmentDropSlot : DropSlot<EquipmentSlot>
    {
        [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
        
        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out EquipmentSlot slot))
                if (slot.EquipmentItem.Type == EquipmentType)
                    Set(slot);
        }
        
        public override void Set(EquipmentSlot slot)
        {
            base.Set(slot);
            
            Rarity.sprite = slot.Rarity.sprite;
        }
    }
}
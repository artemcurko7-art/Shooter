using System;
using Game.Scripts.Equipment.DragInDrop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.EquipmentContext.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public class EquipmentDragSlot : DragSlot<EquipmentSlot>
    {
        public override void OnEndDrag(PointerEventData eventData)
        {
            bool isDropSlot = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out EquipmentDropSlot dropSlot);
            bool isAreaDropSlot = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out AreaDropSlot areaDropSlot);

            if ((isDropSlot == false && isAreaDropSlot == false) || (isDropSlot && dropSlot.EquipmentType != Slot.EquipmentItem.Type))
            {
                ResetSettings();
            }
            else
                base.OnEndDrag(eventData);
        }
    }
}
using System;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Equipment.CharacterContext.DragInDrop
{
    public class CharacterDropSlot : DropSlot<CharacterSlot>
    {
        [field: SerializeField] public CharacterPlaceDropSlotType Type { get; private set; }

        public event Action<CharacterPlaceDropSlotType> TypeDropped;
        
        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out CharacterSlot slot))
            {
                TypeDropped?.Invoke(Type);
                Set(slot);
                Icon.color = Icon.color.GetAlpha(1);
            }
        }

        public override void Set(CharacterSlot slot)
        {
            base.Set(slot);
            
            slot.Icon.color = slot.Icon.color.GetAlpha(0);
        }
    }
}
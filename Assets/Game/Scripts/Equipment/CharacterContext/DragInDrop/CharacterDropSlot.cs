using System;
using System.Collections;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Extensions;
using Game.Scripts.SquadContext.Type;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Equipment.CharacterContext.DragInDrop
{
    public class CharacterDropSlot : DropSlot<CharacterSlot>
    {
        [field: SerializeField] public SquadNumberType Type { get; private set; }

        public event Action<SquadNumberType> TypeDropped;
        
        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out CharacterSlot slot))
            {
                TypeDropped?.Invoke(Type);
                Set(slot);
            }
        }

        public override void Set(CharacterSlot slot)
        {
            base.Set(slot);

            Icon.color = Color.white;
        }
    }
}
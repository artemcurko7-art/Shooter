using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Extensions;
using UnityEngine.EventSystems;

namespace Game.Scripts.Equipment.CharacterContext.DragInDrop
{
    public class CharacterDragSlot : DragSlot<CharacterSlot>
    {
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            MakeIconOpaque();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            bool isDropSlot = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out CharacterDropSlot dropSlot);

            if (isDropSlot == false)
                ResetSettings();
            else
                base.OnEndDrag(eventData);
        }

        public override void ResetSettings()
        {
            base.ResetSettings();
            
            MakeIconOpaque();
        }

        private void MakeIconOpaque()
        {
            Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            Slot.Icon.color = Slot.Icon.color.GetAlpha(1);
        }
    }
}
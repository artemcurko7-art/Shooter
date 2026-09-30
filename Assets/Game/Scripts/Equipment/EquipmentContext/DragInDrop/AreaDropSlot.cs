using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext.DragInDrop
{
    public class AreaDropSlot : MonoBehaviour, IDropHandler
    {
        private EquipmentDropSlot[] _dropSlots;
        
        [Inject]
        public void Construct(EquipmentDropSlot[] dropSlots)
        {
            _dropSlots = dropSlots;
        }

        public void OnDrop(PointerEventData eventData)
        {
            foreach (var dropSlot in _dropSlots)
                dropSlot.OnDrop(eventData);
        }
    }
}
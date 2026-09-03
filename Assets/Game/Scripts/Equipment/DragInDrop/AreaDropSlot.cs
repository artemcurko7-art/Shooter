using Game.Scripts.Equipment.Type;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Scripts.Equipment.DragInDrop
{
    public class AreaDropSlot : MonoBehaviour, IDropHandler
    {
        private DropSlot[] _dropSlots;
        
        [Inject]
        public void Construct(DropSlot[] dropSlots)
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
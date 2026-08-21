using System;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.DragInDrop
{
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image _childRectTransform;
        [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
        
        private RectTransform _rectTransform;
        
        public event Action<Slot> Dropped;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Slot slot))
            {
                if (slot.EquipmentType == EquipmentType)
                {
                    slot.transform.SetParent(transform);
                    slot.transform.localPosition = Vector3.zero;
                    slot.RectTransform.sizeDelta = _rectTransform.sizeDelta;
                    slot.ChildRectTransform.sizeDelta = _childRectTransform.rectTransform.sizeDelta;
                    
                    Dropped?.Invoke(slot);
                }
            }
        }
    }
}
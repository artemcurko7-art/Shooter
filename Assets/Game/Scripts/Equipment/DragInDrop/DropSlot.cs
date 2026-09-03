using System;
using Game.Scripts.Equipment.Type;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.DragInDrop
{
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image _childRectTransform;
        
        [field: SerializeField] public EquipmentType EquipmentType { get; private set; }

        private RectTransform _rectTransform;
        
        public event Action<Slot> Dropped;
        
        public Slot Slot { get; private set; }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Slot slot))
                if (slot.EquipmentItem.Type == EquipmentType)
                    Set(slot);
        }

        public void Set(Slot slot)
        {
            slot.transform.SetParent(transform);
            slot.transform.localPosition = Vector3.zero;
            slot.RectTransform.sizeDelta = _rectTransform.sizeDelta;
            slot.ChildRectTransform.sizeDelta = _childRectTransform.rectTransform.sizeDelta;
            Slot = slot;
            Dropped?.Invoke(slot);
        }

        public void Clear()
        {
            Slot = null;
        }
    }
}
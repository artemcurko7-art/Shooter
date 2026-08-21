using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.DragInDrop
{
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image _childRectTransform;
        [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
        
        private EquipmentService _equipmentService;
        private SortingEquipmentByParameters _sorting;
        private RectTransform _rectTransform;

        [Inject]
        public void Construct(EquipmentService equipmentService, SortingEquipmentByParameters sorting)
        {
            _equipmentService = equipmentService;
            _sorting = sorting;
        }
        
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
                    _equipmentService.RemoveSlot(slot);
                    _sorting.Sort();
                }
            }
        }
    }
}
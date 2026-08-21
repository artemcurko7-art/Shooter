using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
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
        
        private SortingEquipmentByParameters _sortingEquipmentByParameters;
        private RectTransform _rectTransform;

        [Inject]
        public void Construct(SortingEquipmentByParameters sortingEquipmentByParameters)
        {
            _sortingEquipmentByParameters = sortingEquipmentByParameters;
        }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _sortingEquipmentByParameters.Sort();
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
                }
            }
        }
    }
}
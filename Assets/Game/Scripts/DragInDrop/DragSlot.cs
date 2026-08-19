using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Slot _slot;
        
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private int _indexHierarchy;
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _indexHierarchy = _rectTransform.GetSiblingIndex();
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            _gridLayoutGroup.enabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DropSlot dropSlot) == false)
            {
                if (dropSlot.EquipmentType != _slot.EquipmentType)
                {
                    _rectTransform.SetParent(_gridLayoutGroup.transform);
                    _rectTransform.SetSiblingIndex(_indexHierarchy);
                    Debug.Log("Отпустил");
                }
                
                Debug.Log($"Drop slot type: {dropSlot.EquipmentType}, slot: {_slot.EquipmentType}");
            }
            
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.enabled = true;
        }
    }
}
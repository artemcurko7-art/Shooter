using System;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Slot _slot;
        
        private LazyInject<EquipmentService> _equipmentService;
        private LazyInject<SortingEquipmentByParameters> _sorting;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private Vector2 _sizeDelta;
        private int _indexHierarchy;

        [Inject]
        public void Construct(LazyInject<EquipmentService> equipmentService, LazyInject<SortingEquipmentByParameters> sorting)
        {
            _equipmentService = equipmentService;
            _sorting = sorting;
        }
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _sizeDelta = _slot.ChildRectTransform.sizeDelta;
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
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DropSlot dropSlot) == false || dropSlot.EquipmentType != _slot.EquipmentType)
            {
                _rectTransform.SetParent(_gridLayoutGroup.transform);
                _rectTransform.SetSiblingIndex(_indexHierarchy);
                _slot.ChildRectTransform.sizeDelta = _sizeDelta;

                if (_equipmentService.Value.IsCheckerSlot(_slot) == false)
                {
                    _equipmentService.Value.AddSlot(_slot);
                    _sorting.Value.Sort();
                }
            }
            
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.enabled = true;
        }
    }
}
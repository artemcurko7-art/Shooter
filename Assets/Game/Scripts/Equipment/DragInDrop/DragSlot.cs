using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Slot _slot;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup; 
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private Transform _parent;
        private Vector2 _sizeDelta;
        private int _indexHierarchy;

        public event Action<Slot> BeginDragged;
        public event Action<Slot> EndDragged;
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _parent = _rectTransform.parent;
        }

        public void Initialize(Slot slot)
        {
            _slot = slot;
            _sizeDelta = _slot.ChildRectTransform.sizeDelta;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _indexHierarchy = _rectTransform.GetSiblingIndex();
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            _gridLayoutGroup.enabled = false;
            
            BeginDragged?.Invoke(_slot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            bool isDropSlot = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DropSlot dropSlot);
            
            if (isDropSlot == false || dropSlot.EquipmentType != _slot.EquipmentType)
            {
                ResetSettings();
                
                EndDragged?.Invoke(_slot);
            }
            
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.enabled = true;
        }

        public void ResetSettings()
        {
            _rectTransform.SetParent(_parent);
            _rectTransform.SetSiblingIndex(_indexHierarchy);
            _slot.ChildRectTransform.sizeDelta = _sizeDelta;
            
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.enabled = true;
        }
    }
}
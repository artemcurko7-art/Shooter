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
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private Vector2 _sizeDelta;
        private int _indexHierarchy;
        
        public event Action<Slot> BeginDragged;
        public event Action<Slot> EndDragged;
        
        public CanvasGroup CanvasGroup { get; private set; }
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            CanvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
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
            CanvasGroup.blocksRaycasts = false;
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
                ResetSettings();
            else
                EndDragged?.Invoke(_slot);
        }

        public void ResetSettings()
        {
            _rectTransform.SetParent(_gridLayoutGroup.transform);
            _rectTransform.SetSiblingIndex(_indexHierarchy);
            _slot.ChildRectTransform.sizeDelta = _sizeDelta;
            EndDragged?.Invoke(_slot);
        }
    }
}
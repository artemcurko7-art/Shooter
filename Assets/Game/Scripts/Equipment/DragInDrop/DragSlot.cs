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
            // bool isDropSlot = eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DropSlot dropSlot);
            // Debug.Log($"Event {eventData.pointerCurrentRaycast.gameObject.name}");
            // if (isDropSlot)
            // {
            //     dropSlot.DisableBusy();
            //     Debug.Log("Отключение занятости");
            // }
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
                _rectTransform.SetParent(_gridLayoutGroup.transform);
                _rectTransform.SetSiblingIndex(_indexHierarchy);
                _slot.ChildRectTransform.sizeDelta = _sizeDelta;
                
                EndDragged?.Invoke(_slot);
            }
            else if (dropSlot)
            {
                Debug.Log($"On end drag");
            }
            
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.enabled = true;
        }
    }
}
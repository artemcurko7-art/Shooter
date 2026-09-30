using System;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class DragSlot<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : Slot
    {
        protected T Slot;
        private Canvas _canvas;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private Vector2 _sizeDelta;
        private int _indexHierarchy;
        
        public event Action<T> BeginDragged;
        public event Action<T> EndDragged;
        
        public CanvasGroup CanvasGroup { get; private set; }
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            CanvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(T slot)
        {
            Slot = slot;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _indexHierarchy = _rectTransform.GetSiblingIndex();
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            CanvasGroup.blocksRaycasts = false;
            Slot.Icon.color = Slot.Icon.color.GetAlpha(1);
            Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            
            BeginDragged?.Invoke(Slot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            EndDragged?.Invoke(Slot);
        }

        public virtual void ResetSettings()
        {
            _rectTransform.SetParent(_gridLayoutGroup.transform);
            _rectTransform.SetSiblingIndex(_indexHierarchy);
            Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            EndDragged?.Invoke(Slot);
        }
    }
}
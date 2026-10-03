using System;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.DragInDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class DragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
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

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _indexHierarchy = _rectTransform.GetSiblingIndex();
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            CanvasGroup.blocksRaycasts = false;
            //Slot.Icon.color = Slot.Icon.color.GetAlpha(1);
            //Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            
            //BeginDragged?.Invoke(Slot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            //EndDragged?.Invoke(Slot);
        }

        public virtual void ResetSettings()
        {
            _rectTransform.SetParent(_gridLayoutGroup.transform);
            _rectTransform.SetSiblingIndex(_indexHierarchy);
            //Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            //EndDragged?.Invoke(Slot);
        }
        
        protected void InvokeBeginDragged(Slot slot) => BeginDragged?.Invoke(slot);
        protected void InvokeEndDragged(Slot slot) => EndDragged?.Invoke(slot);
    }

    public abstract class DragSlot<TSlot> : DragSlot where TSlot : Slot
    {
        protected TSlot Slot { get; private set; }
        
        public void Initialize(TSlot slot)
        {
            Slot = slot;
        }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            
            Slot.Icon.color = Slot.Icon.color.GetAlpha(1);
            Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            InvokeBeginDragged(Slot);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            InvokeEndDragged(Slot);
        }

        public override void ResetSettings()
        {
            base.ResetSettings();
            
            Slot.Rarity.color = Slot.Rarity.color.GetAlpha(1);
            //Slot.Icon.color = Slot.Icon.color.GetAlpha(1);
            InvokeEndDragged(Slot);
        }
    }
}
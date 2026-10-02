using System;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.DragInDrop
{
    public abstract class DropSlot<T> : MonoBehaviour, IDropHandler where T : Slot
    {
        private RectTransform _rectTransform;
        private Sprite _currentRarity;
        private Sprite _currentIcon;
        
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] protected Image Rarity { get; private set; }
        
        
        public T Slot { get; private set; }
        
        public event Action<T> Dropped;
        public event Action<T> Removed;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _currentIcon = Icon.sprite;
            _currentRarity = Rarity.sprite;
        }

        public abstract void OnDrop(PointerEventData eventData);

        public virtual void Set(T slot)
        {
            slot.transform.SetParent(transform);
            slot.transform.localPosition = Vector3.zero;
            slot.RectTransform.sizeDelta = _rectTransform.sizeDelta;
            Icon.sprite = slot.Icon.sprite;
            Icon.color = Icon.color.GetAlpha(1);
            slot.Rarity.color = slot.Rarity.color.GetAlpha(0);
            slot.Icon.color = slot.Icon.color.GetAlpha(0);
            Slot = slot;
            Dropped?.Invoke(slot);
        }

        public void Clear()
        {
            Removed?.Invoke(Slot);
            Icon.sprite = null;
            Icon.sprite = _currentIcon;
            Rarity.sprite = _currentRarity;
            Icon.color = Icon.color.GetAlpha(0);
            Slot = null;
        }
    }
}
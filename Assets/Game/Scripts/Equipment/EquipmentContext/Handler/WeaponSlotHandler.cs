using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Provider;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext.Handler
{
    public class WeaponSlotHandler : ISubscriber
    {
        private readonly EquipmentDropSlot _dropSlot;
        private readonly WeaponData _data;
        private readonly WeaponProvider _provider;
        
        public WeaponSlotHandler(EquipmentDropSlot dropSlot, WeaponData data, WeaponProvider provider)
        {
            _dropSlot = dropSlot;
            _data = data;
            _provider = provider;
        }
        
        public void Subscribe()
        {
            _dropSlot.Dropped += OnDropped;
        }

        public void Unsubscribe()
        {
            _dropSlot.Dropped -= OnDropped;
        }

        private void OnDropped(EquipmentSlot slot)
        {
            _provider.Set(_data.Weapons[slot.EquipmentItem.WeaponType]);
            Debug.Log($"Weapon type: {slot.EquipmentItem.WeaponType}");
        }
    }
}
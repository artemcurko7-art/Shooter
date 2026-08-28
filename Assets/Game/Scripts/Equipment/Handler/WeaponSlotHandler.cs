using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Provider;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Data;
using UnityEngine;

namespace Game.Scripts.Equipment.Handler
{
    public class WeaponSlotHandler : ISubscriber
    {
        private readonly DropSlot _dropSlot;
        private readonly IWeaponData _data;
        private readonly WeaponProvider _provider;
        
        public WeaponSlotHandler(DropSlot dropSlot, IWeaponData data, WeaponProvider provider)
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

        private void OnDropped(Slot slot)
        {
            _provider.Set(_data.Weapons[slot.EquipmentItem.WeaponType]);
            Debug.Log($"Weapon type: {slot.EquipmentItem.WeaponType}");
        }
    }
}
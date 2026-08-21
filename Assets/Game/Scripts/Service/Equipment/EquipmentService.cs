using System;
using System.Collections.Generic;
using Game.Scripts.DragInDrop;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class EquipmentService : ISubscriber
    {
        private readonly EquipmentData _data;
        private readonly RarityEquipmentData _rarityData;
        private readonly SlotFactory _slotFactory;
        private readonly DropSlot[] _dropSlots;
        private readonly SortingEquipmentByParameters _sorting;
        private readonly List<Slot> _slots = new();
        private readonly Transform _container;
        
        public EquipmentService(RarityEquipmentData rarityData, EquipmentData data, SlotFactory slotFactory, DropSlot[] dropSlots, SortingEquipmentByParameters sorting, Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _slotFactory = slotFactory;
            _dropSlots = dropSlots;
            _sorting = sorting;
            _container = container;
            
            Create();
            _sorting.Sort(_slots);
        }

        public void Subscribe()
        {
            foreach (var slot in _slots)
                slot.Drag.Dragged += OnDraggedSlot;

            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped += OnDroppedSlot;
        }

        public void Unsubscribe()
        {
            foreach (var slot in _slots)
                slot.Drag.Dragged -= OnDraggedSlot;
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.Dropped -= OnDroppedSlot;
        }

        private void OnDraggedSlot(Slot slot)
        {
            if (_slots.Contains(slot) == false)
            {
                _slots.Add(slot);
                _sorting.Sort(_slots);
            }
        }

        private void OnDroppedSlot(Slot slot)
        {
            _slots.Remove(slot);
            _sorting.Sort(_slots);
        }
        
        private void Create() // тест в дальнейшем исправим(переписать логику)
        {
            CreateUsual();
            CreateUnusual();
            CreateRare();
            CreateMythical();
            CreateEpic();
            CreateLegendary();
        }

        private void CreateUsual()
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Weapon][0], _container));
        }
        
        private void CreateUnusual()
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
        
        private void CreateRare()
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateEpic()
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateLegendary()
        
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        private void CreateMythical()
        {
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
    }
}
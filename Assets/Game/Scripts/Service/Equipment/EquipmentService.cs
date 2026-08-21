using System;
using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class EquipmentService
    {
        private readonly EquipmentData _data;
        private readonly RarityEquipmentData _rarityData;
        private readonly SlotFactory _rarityFactory;
        private readonly List<Slot> _slots = new();
        private readonly Transform _container;
        
        public EquipmentService(EquipmentData data, RarityEquipmentData rarityData, SlotFactory rarityFactory, Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _rarityFactory = rarityFactory;
            _container = container;
            
            Create();
        }

        public List<Slot> Slots => _slots;

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
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Weapon][0], _container));
        }
        
        private void CreateUnusual()
        {
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
        
        private void CreateRare()
        {
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateEpic()
        {
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateLegendary()
        
        {
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        private void CreateMythical()
        {
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slots.Add(_rarityFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
    }
}
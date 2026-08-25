using System;
using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class EquipmentService : IEquipmentService
    {
        private readonly EquipmentData _data;
        private readonly RarityEquipmentData _rarityData;
        private readonly SlotHandler _slotHandler;
        private readonly SlotFactory _slotFactory;
        private readonly Transform _container;
        
        public EquipmentService(
            RarityEquipmentData rarityData, 
            EquipmentData data, 
            SlotHandler slotHandler,
            SlotFactory slotFactory, 
            Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _slotHandler = slotHandler;
            _slotFactory = slotFactory;
            _container = container;
            
            Create();
        }
        
        public IReadOnlyList<Slot> Slots => _slotHandler.Slots;

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
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Usual],
                _data.Configs[EquipmentType.Weapon][0], _container));
        }
        
        private void CreateUnusual()
        {
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Unusual],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
        
        private void CreateRare()
        {
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Rare],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateEpic()
        {
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Epic],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        
        private void CreateLegendary()
        
        {
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Suit][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Legendary],
                _data.Configs[EquipmentType.Boots][0], _container));
        }
        private void CreateMythical()
        {
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Weapon][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Amulet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Gloves][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Boots][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Helmet][0], _container));
            _slotHandler.Add(_slotFactory.Create(_rarityData.Configs[RarityEquipmentType.Mythical],
                _data.Configs[EquipmentType.Suit][0], _container));
        }
    }
}
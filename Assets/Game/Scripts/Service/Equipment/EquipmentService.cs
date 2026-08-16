using System;
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
        private readonly RarityEquipmentViewFactory _rarityFactory;
        private readonly Transform _container;
        
        public EquipmentService(EquipmentData data, RarityEquipmentData rarityData, RarityEquipmentViewFactory rarityFactory, Transform container)
        {
            _data = data;
            _rarityData = rarityData;
            _rarityFactory = rarityFactory;
            _container = container;
            
            Create();
        }

        private void Create() // тест в дальнейшем исправим(переписать логику)
        {
            for (int i = 0; i < _data.Configs[EquipmentType.Weapon].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Legendary],
                    _data.Configs[EquipmentType.Weapon][i].Icon, _container);
            }
            
            for (int i = 0; i < _data.Configs[EquipmentType.Helmet].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Epic],
                    _data.Configs[EquipmentType.Helmet][i].Icon, _container);
            }
            
            for (int i = 0; i < _data.Configs[EquipmentType.Amulet].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Rare],
                    _data.Configs[EquipmentType.Amulet][i].Icon, _container);
            }
            
            for (int i = 0; i < _data.Configs[EquipmentType.Boots].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Unusual],
                    _data.Configs[EquipmentType.Boots][i].Icon, _container);
            }
            
            for (int i = 0; i < _data.Configs[EquipmentType.Suit].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Usual],
                    _data.Configs[EquipmentType.Suit][i].Icon, _container);
            }
            
            for (int i = 0; i < _data.Configs[EquipmentType.Gloves].Count; i++)
            {
                _rarityFactory.Create(_rarityData.Views[RarityEquipmentType.Usual],
                    _data.Configs[EquipmentType.Gloves][i].Icon, _container);
            }
        }
    }
}
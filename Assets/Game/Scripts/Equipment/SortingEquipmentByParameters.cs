using System;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Service.Equipment;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public class SortingEquipmentByParameters 
    {
        private readonly EquipmentService _equipmentService;
        private readonly Transform _container;
        private int _index;
        
        public SortingEquipmentByParameters(EquipmentService equipmentService, Transform container)
        {
            _equipmentService = equipmentService;
            _container = container;
            
            Sort();
        }
        
        public void Sort()
        {
            _index = _equipmentService.Slots.Count - 1;
            
            foreach (var rarityType in Enum.GetValues(typeof(RarityEquipmentType)))
            {
                if ((RarityEquipmentType)rarityType == RarityEquipmentType.None)
                    continue;
                
                var types = Enum.GetValues(typeof(EquipmentType));
                
                for (int type = types.Length - 1; type >= 0; type--)
                {
                    if ((EquipmentType)type == EquipmentType.None)
                        continue;
                
                    foreach (var slot in _equipmentService.Slots)
                    {
                        if ((RarityEquipmentType)rarityType == slot.RarityEquipmentType && (EquipmentType)type == slot.EquipmentType)
                        {
                            slot.transform.SetSiblingIndex(_index);
                            _index--;
                        }
                    }
                }
            }
        }
    }
}
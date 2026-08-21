using System;
using System.Collections.Generic;
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
        }

        public void Sort()
        {
            _index = _equipmentService.Slots.Count - 1;
            
            foreach (var rarityType in Enum.GetValues(typeof(RarityEquipmentType)))
            {
                if ((RarityEquipmentType)rarityType == RarityEquipmentType.None)
                    continue;
                
                for (int i = 0; i < _equipmentService.Slots.Count; i++)
                {
                    for (int j = 0; j < _equipmentService.Slots.Count; j++)
                    {
                        if ((RarityEquipmentType)rarityType == _equipmentService.Slots[i].RarityEquipmentType)
                        {
                            _equipmentService.Slots[i].transform.SetSiblingIndex(_index);
                            _index--;
                        }  
                    }  
                }
                
                // foreach (var type in Enum.GetValues(typeof(EquipmentType)))
                // {
                //     if ((EquipmentType)type == EquipmentType.None)
                //         continue;
                //     
                //     for (int i = 0; i < _equipmentService.Slots.Count; i++)
                //     {
                //         for (int j = 0; j < _equipmentService.Slots.Count; j++)
                //         {
                //             if ((RarityEquipmentType)rarityType == _equipmentService.Slots[i].RarityEquipmentType && 
                //                 (EquipmentType)type == _equipmentService.Slots[i].EquipmentType)
                //             {
                //                 _equipmentService.Slots[i].transform.SetSiblingIndex(_index);
                //                 _index--;
                //             }  
                //         }  
                //     }
                // }
            }
            
            Debug.Log($"index: {_index}");
        }
    }
}
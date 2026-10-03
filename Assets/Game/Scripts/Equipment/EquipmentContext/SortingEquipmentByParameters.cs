using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class SortingEquipmentByParameters 
    {
        private readonly Transform _container;
        private int _index;
        
        public SortingEquipmentByParameters(Transform container)
        {
            _container = container;
        }
        
        public void Sort(IReadOnlyList<EquipmentSlot> slots)
        {
            _index = slots.Count - 1;

            foreach (var rarityType in Enum.GetValues(typeof(RarityEquipmentType)))
            {
                if ((RarityEquipmentType)rarityType == RarityEquipmentType.None)
                    continue;
                
                var types = Enum.GetValues(typeof(EquipmentType));
                
                for (int type = types.Length - 1; type >= 0; type--)
                {
                    if ((EquipmentType)type == EquipmentType.None)
                        continue;
                    
                    foreach (var slot in slots)
                    {
                        if ((RarityEquipmentType)rarityType == slot.RarityConfig.Type && (EquipmentType)type == slot.EquipmentItem.Type)
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
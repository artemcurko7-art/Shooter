using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.EquipmentContext.Type;

namespace Game.Scripts.Equipment.EquipmentContext.Provider
{
    public class EquipmentProvider : IEquipmentProvider
    {
        private readonly Dictionary<EquipmentType, EquipmentItem> _equipmentItems = new();
        
        public EquipmentProvider()
        {
            foreach (var type in Enum.GetValues(typeof(EquipmentType)))
            {
                if ((EquipmentType)type == EquipmentType.None)
                    continue;
                
                _equipmentItems.Add((EquipmentType)type, null);
            }
        }
        
        public IReadOnlyDictionary<EquipmentType, EquipmentItem> EquipmentItems => _equipmentItems;

        public void Set(EquipmentType type, EquipmentItem item)
        {
            _equipmentItems[type] = item;
        }

        public void Remove(EquipmentType type)
        {
            _equipmentItems.Remove(type);
        }
    }
}
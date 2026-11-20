using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.EquipmentContext.Data
{
    public class EquipmentData
    {
        private readonly EquipmentConfig[] _configs;
        private readonly Dictionary<EquipmentType, List<EquipmentConfig>> _equipments = new();

        public EquipmentData()
        {
            _configs = Resources.LoadAll<EquipmentConfig>("Configs/Equipment");

            Fill();
        }

        public IReadOnlyDictionary<EquipmentType, List<EquipmentConfig>> Equipments => _equipments;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == EquipmentType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_equipments.ContainsKey(config.Type) == false)
                    _equipments.Add(config.Type, new List<EquipmentConfig>());

                _equipments[config.Type].Add(config);
            }
        }
    }
}
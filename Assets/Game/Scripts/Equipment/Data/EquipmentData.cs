using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.Data
{
    public class EquipmentData
    {
        private readonly EquipmentConfig[] _configs;
        private readonly Dictionary<EquipmentType, List<EquipmentConfig>> _equipmentConfigs = new();

        public EquipmentData()
        {
            _configs = Resources.LoadAll<EquipmentConfig>("Configs/Equipment");

            Fill();
        }

        public IReadOnlyDictionary<EquipmentType, List<EquipmentConfig>> Configs => _equipmentConfigs;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == EquipmentType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_equipmentConfigs.ContainsKey(config.Type) == false)
                    _equipmentConfigs.Add(config.Type, new List<EquipmentConfig>());

                _equipmentConfigs[config.Type].Add(config);
            }
        }
    }
}
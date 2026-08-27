using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.MV.Stat.Data
{
    public class StatData
    {
        private readonly StatConfig[] _additionalConfigs;
        private readonly Dictionary<EquipmentType, StatConfig> _additionalStats = new();
        private readonly StatConfig[] _mainConfigs;
        private readonly Dictionary<EquipmentType, StatConfig> _mainStats = new();

        public StatData()
        {
            // _mainStates.Add(StatType.Health, "HP");
            // _mainStates.Add(StatType.Attack, "ATK");
            // _mainStates.Add(StatType.Defence, "DEF");
            // _mainStates.Add(StatType.CriticalChance, "C. RATE");
            // _mainStates.Add(StatType.CriticalDamage, "C. DMG");

            _mainConfigs = Resources.LoadAll<StatConfig>("Configs/Stat/Main");
            _additionalConfigs = Resources.LoadAll<StatConfig>("Configs/Stat/Additional");

            Fill();
        }

        public IReadOnlyDictionary<EquipmentType, StatConfig> MainStats => _mainStats;
        public IReadOnlyDictionary<EquipmentType, StatConfig> AdditionalStats => _additionalStats;

        private void Fill()
        {
            foreach (var mainConfig in _mainConfigs)
            {
                if (mainConfig.Type == EquipmentType.None)
                    throw new InvalidOperationException($"Not type: {mainConfig.Type}");

                if (_mainStats.ContainsKey(mainConfig.Type))
                    throw new InvalidOperationException($"Duplicate type: {mainConfig.Type}");

                _mainStats.Add(mainConfig.Type, mainConfig);
            }

            foreach (var additionalConfig in _additionalConfigs)
            {
                if (additionalConfig.Type == EquipmentType.None)
                    throw new InvalidOperationException($"Not type: {additionalConfig.Type}");
            
                if (_additionalStats.ContainsKey(additionalConfig.Type))
                    throw new InvalidOperationException($"Duplicate type: {additionalConfig.Type}");
            
                _additionalStats.Add(additionalConfig.Type, additionalConfig);
            }
        }
    }
}
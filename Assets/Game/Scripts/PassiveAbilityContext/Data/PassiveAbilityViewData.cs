using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Data
{
    public class PassiveAbilityViewData : IPassiveAbilityViewData
    {
        private readonly PassiveAbilityViewConfig[] _configs;
        private readonly Dictionary<StatType, PassiveAbilityViewConfig> _views = new();
        
        public PassiveAbilityViewData()
        {
            _configs = Resources.LoadAll<PassiveAbilityViewConfig>("Configs/PassiveAbilityView");
            
            Fill();
        }

        public IReadOnlyDictionary<StatType, PassiveAbilityViewConfig> Views => _views;
        
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.StatType == StatType.None)
                    throw new InvalidOperationException($"Not type: {config.StatType}");

                if (_views.ContainsKey(config.StatType))
                    throw new InvalidOperationException($"Duplicate type: {config.StatType}");

                _views.Add(config.StatType, config);
            }
        }
    }
}
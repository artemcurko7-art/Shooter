using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.BodyIK
{
    public class BodyInverseKinematicData
    {
        private readonly Dictionary<WeaponType, BodyInverseKinematicConfig> _settings = new();
        private readonly BodyInverseKinematicConfig[] _configs;
    
        public BodyInverseKinematicData()
        {
            _configs = Resources.LoadAll<BodyInverseKinematicConfig>("Configs/BodyIK");
        
            Fill();
        }

        public IReadOnlyDictionary<WeaponType, BodyInverseKinematicConfig> Settings => _settings;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == WeaponType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");
            
                if (_settings.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
            
                _settings.Add(config.Type, config);
            }
        }
    }
}
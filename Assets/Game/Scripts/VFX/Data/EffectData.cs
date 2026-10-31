using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.VFX.Type;
using UnityEngine;

namespace Game.Scripts.VFX.Data
{
    public class EffectData : IEffectData
    {
        private readonly EffectConfig[] _configs;
        private readonly Dictionary<EffectType, Effect> _effects = new();
        
        public EffectData()
        {
            _configs = Resources.LoadAll<EffectConfig>("Configs/Effect");
            
            Fill();
        }

        public IReadOnlyDictionary<EffectType, Effect> Effects => _effects;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == EffectType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_effects.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");

                _effects.Add(config.Type, config.Effect);
            }
        }
    }
}
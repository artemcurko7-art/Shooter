using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.PassiveAbility.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbility.Data
{
    public class PassiveAbilityData : IPassiveAbilityData
    {
        private readonly PassiveAbilityConfig[] _configs;
        private readonly Dictionary<PassiveAbilityType, PassiveAbilityConfig> _passiveAbilities = new();

        public PassiveAbilityData()
        {
            _configs = Resources.LoadAll<PassiveAbilityConfig>("Configs/PassiveAbility");

            Fill();
        }

        public IReadOnlyDictionary<PassiveAbilityType, PassiveAbilityConfig> PassiveAbilities => _passiveAbilities;

        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == PassiveAbilityType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");

                if (_passiveAbilities.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");

                _passiveAbilities.Add(config.Type, config);
            }
        }
    }
}
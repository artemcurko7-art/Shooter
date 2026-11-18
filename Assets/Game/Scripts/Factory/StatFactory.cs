using System;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Factory
{
    public class StatFactory
    {
        private readonly IStatRegistry _registry;
        
        public StatFactory(IStatRegistry registry)
        {
            _registry = registry;
        }

        public Stat Create(StatType type, int value, bool isPercentage)
        {
            if (_registry.Types.TryGetValue(type, out var statType) == false)
            {
                Debug.LogError($"Unknown StatType: {type}");
                return null;
            }
            
            return (Stat)Activator.CreateInstance(statType, value, isPercentage);
        }
    }
}
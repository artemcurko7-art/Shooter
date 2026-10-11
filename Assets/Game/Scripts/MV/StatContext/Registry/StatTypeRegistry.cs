using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.MV.StatContext.Repository
{
    public class StatTypeRegistry : IStatTypeRegistry
    {
        private readonly Dictionary<StatType, System.Type> _types = new();
        
        public StatTypeRegistry()
        {
            Fill();
        }

        public IReadOnlyDictionary<StatType, System.Type> Types => _types;
        
        private void Fill()
        {
            foreach (var type in ReflectionExtensions.GetImplementations<Stat>())
            {
                var attribute = type.GetCustomAttribute<StatTypeAttribute>();

                if (attribute.Type == StatType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to StatType.None!");
                }
                
                if (_types.TryAdd(attribute.Type, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate StatType registration for '{attribute.Type}'. " +
                        $"Conflict between '{_types[attribute.Type].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
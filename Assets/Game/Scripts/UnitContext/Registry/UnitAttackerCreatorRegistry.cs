using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Creator;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Registry
{
    public class UnitAttackerCreatorRegistry : IUnitAttackerCreatorRegistry
    {
        private readonly Dictionary<UnitAttackerType, System.Type> _types = new();
        
        public UnitAttackerCreatorRegistry()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<UnitAttackerType, System.Type> Types => _types;
        
        private void Fill()
        {
            foreach (var type in ReflectionExtensions.GetImplementations<IAttackerCreator>())
            {
                var attribute = type.GetCustomAttribute<UnitAttackerTypeAttribute>();

                if (attribute.Type == UnitAttackerType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to UnitAttackerType.None!");
                }

                if (_types.TryAdd(attribute.Type, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate UnitAttackerType registration for '{attribute.Type}'. " +
                        $"Conflict between '{_types[attribute.Type].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
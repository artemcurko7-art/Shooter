using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Registry
{
    public class PassiveAbilityRegistry : IPassiveAbilityRegistry
    {
        private readonly Dictionary<PassiveAbilityType, System.Type> _types = new();
        
        public PassiveAbilityRegistry()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<PassiveAbilityType, System.Type> Types => _types;
        
        private void Fill()
        {
            foreach (var type in ReflectionExtensions.GetImplementations<IPassiveAbility>())
            {
                var attribute = type.GetCustomAttribute<PassiveAbilityTypeAttribute>();

                if (attribute.Type == PassiveAbilityType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to PassiveAbilityType.None!");
                }

                if (_types.TryAdd(attribute.Type, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate PassiveAbilityType registration for '{attribute.Type}'. " +
                        $"Conflict between '{_types[attribute.Type].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
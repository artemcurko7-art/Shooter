using System;
using System.Collections.Generic;
using System.Reflection;
using Game.Scripts.Extensions;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Targeting;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Registry
{
    public class PassiveAbilityTargetingRegistry : IPassiveAbilityTargetingRegistry
    {
        private readonly Dictionary<PassiveAbilityTargetingType, System.Type> _types = new();
        
        public PassiveAbilityTargetingRegistry()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<PassiveAbilityTargetingType, System.Type> Types => _types;
        
        private void Fill()
        {
            foreach (var type in ReflectionExtensions.GetImplementations<IPassiveAbilityTargeting>())
            {
                var attribute = type.GetCustomAttribute<PassiveAbilityTargetingTypeAttribute>();

                if (attribute.Type == PassiveAbilityTargetingType.None)
                {
                    throw new InvalidOperationException(
                        $"Class '{type.Name}' cannot be assigned to PassiveAbilityTargetingType.None!");
                }

                if (_types.TryAdd(attribute.Type, type) == false)
                {
                    throw new InvalidOperationException(
                        $"Duplicate PassiveAbilityTargetingType registration for '{attribute.Type}'. " +
                        $"Conflict between '{_types[attribute.Type].Name}' and '{type.Name}'.");
                }
            }
        }
    }
}
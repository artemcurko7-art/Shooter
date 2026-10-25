using System;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Registry;
using Game.Scripts.PassiveAbilityContext.Targeting;
using Game.Scripts.PassiveAbilityContext.Type;
using Zenject;

namespace Game.Scripts.Factory
{
    public class PassiveAbilityFactory
    {
        private readonly DiContainer _container;
        private readonly IPassiveAbilityRegistry _registry;
        private readonly IPassiveAbilityTargetingRegistry _targetingRegistry;
        
        public PassiveAbilityFactory(
            IPassiveAbilityRegistry registry,
            IPassiveAbilityTargetingRegistry targetingRegistry,
            DiContainer container)
        {
            _registry = registry;
            _targetingRegistry = targetingRegistry;
            _container = container;
        }
        
        public PassiveAbility Create(PassiveAbilityData data)
        {
            var passiveAbility = new PassiveAbility(
                (IPassiveAbility)Activator.CreateInstance(_registry.Types[data.Type]),
                (IPassiveAbilityTargeting)Activator.CreateInstance(_targetingRegistry.Types[data.TargetingType]));
            
            return passiveAbility;
        }
    }
}
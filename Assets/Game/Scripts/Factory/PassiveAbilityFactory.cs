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
            PassiveAbility passiveAbility = null;
            
            switch (data.Type)
            {
                case PassiveAbilityType.Constant:
                    
                    passiveAbility = new PassiveAbility(
                        (ConstantPassiveAbility)Activator.CreateInstance(_registry.Types[data.Type], data.StatType, data.Value),
                        (IPassiveAbilityTargeting)Activator.CreateInstance(_targetingRegistry.Types[data.TargetingType]));
                    
                    break;
                case PassiveAbilityType.Temporary:
                    
                    passiveAbility = new PassiveAbility(
                        (TemporaryPassiveAbility)Activator.CreateInstance(_registry.Types[data.Type], data.StatType, data.Value, data.Cooldown),
                        (IPassiveAbilityTargeting)Activator.CreateInstance(_targetingRegistry.Types[data.TargetingType]));
                    
                    break;
            }
            
            return passiveAbility;
        }
        
        // public PassiveAbility Create(PassiveAbilityData data)
        // {
        //     var passiveAbility = new PassiveAbility(
        //         (IPassiveAbility)Activator.CreateInstance(_registry.Types[data.Type]),
        //         (IPassiveAbilityTargeting)Activator.CreateInstance(_targetingRegistry.Types[data.TargetingType]));
        //     
        //     return passiveAbility;
        // }
    }
}
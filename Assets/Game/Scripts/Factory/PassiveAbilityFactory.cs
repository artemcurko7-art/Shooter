using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Creator;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Registry;
using Game.Scripts.PassiveAbilityContext.Targeting;
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
            var creator = (IPassiveAbilityCreator)_container.Instantiate(_registry.Types[data.Type]);
            var effect = creator.Create(data);
            var targeting = (IPassiveAbilityTargeting)_container.Instantiate(_targetingRegistry.Types[data.TargetingType]);
            
            return new PassiveAbility(effect, targeting);
        }
    }
}
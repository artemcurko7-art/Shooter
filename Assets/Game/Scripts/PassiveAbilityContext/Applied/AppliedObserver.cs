using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.Service.PassiveAbilityContext;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.PassiveAbilityContext.Applied
{
    public abstract class AppliedObserver : ISubscriber
    {
        private readonly IPassiveAbilityService _passiveAbilityService;
        
        public AppliedObserver(IPassiveAbilityService passiveAbilityService)
        {
            _passiveAbilityService = passiveAbilityService;
        }
        
        public void Subscribe()
        {
            foreach(var effect in _passiveAbilityService.Effects)
                effect.Effect.Applied += OnApplied;
        }

        public void Unsubscribe()
        {
            foreach(var effect in _passiveAbilityService.Effects)
                effect.Effect.Applied -= OnApplied;
        }
        
        protected abstract void OnApplied(IAttacker attacker, PassiveAbilityData passiveAbilityData);
    }
}
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Factory;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Repository;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Service.PassiveAbilityContext
{
    public class PassiveAbilityService : IPassiveAbilityService, ISubscriber
    {
        private readonly ICharacterStatResolver _characterStatResolver;
        private readonly IPassiveAbilityDataRepository _repository;
        private readonly PassiveAbilityFactory _factory;
        private readonly List<PassiveAbility> _effects = new();
        
        public PassiveAbilityService(
            ICharacterStatResolver characterStatResolver,
            IPassiveAbilityDataRepository repository,
            PassiveAbilityFactory factory)
        {
            _characterStatResolver = characterStatResolver;
            _repository = repository;
            _factory = factory;
        }

        public IReadOnlyList<PassiveAbility> Effects => _effects;

        public void Subscribe()
        {
            Fill();
            
            for (int i = 0; i < _characterStatResolver.Characters.Count; i++) // test
                _effects[i].Apply(
                    _characterStatResolver.Characters[i],
                    _characterStatResolver.Characters,
                    _repository.PassiveAbilityDates[_characterStatResolver.Characters[i].Type]);
        }

        public void Unsubscribe()
        {
            
        }
        
        private void Fill()
        {
            foreach (var character in _characterStatResolver.Characters)
            {
                var passiveAbility = _factory.Create(_repository.PassiveAbilityDates[character.Type]);
                _effects.Add(passiveAbility);
            }
        }
    }
}
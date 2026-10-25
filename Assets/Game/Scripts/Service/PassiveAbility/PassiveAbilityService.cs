using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Factory;

namespace Game.Scripts.Service.PassiveAbility
{
    public class PassiveAbilityService
    {
        private readonly ICharacterStatResolver _characterStatResolver;
        private readonly PassiveAbilityFactory _factory;
        
        public PassiveAbilityService(ICharacterStatResolver characterStatResolver, PassiveAbilityFactory factory)
        {
            _characterStatResolver = characterStatResolver;
            _factory = factory;
            
            Fill();
        }

        private void Fill()
        {
            foreach (var character in _characterStatResolver.Characters)
            {
                var passiveAbility = _factory.Create(character.PassiveAbilityData);

                passiveAbility.Apply(character, _characterStatResolver.Characters);
            }
        }
    }
}
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.Service.Equipment.CharacterContext;

namespace Game.Scripts.PassiveAbilityContext.Repository
{
    public class PassiveAbilityDataRepository : IPassiveAbilityDataRepository
    {
        private readonly ICharacterData _characterData;
        private readonly Dictionary<CharacterType, PassiveAbilityData> _passiveAbilityDates = new();
        
        public PassiveAbilityDataRepository(ICharacterData characterData)
        {
            _characterData = characterData;
            
            Fill();
        }
        
        public IReadOnlyDictionary<CharacterType, PassiveAbilityData> PassiveAbilityDates => _passiveAbilityDates;

        private void Fill()
        {
            foreach (var character in _characterData.Characters)
                _passiveAbilityDates.Add(character.Key, character.Value.PassiveAbilityData);
        }
    }
}
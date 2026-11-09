using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Modifer;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Creator
{
    [PassiveAbilityType(PassiveAbilityType.RegenerationHealth)]
    public class RegenerationHealthPassiveAbilityCreator : IPassiveAbilityCreator
    {
        private readonly ICharacterHealthResolver _healthResolver;
        private readonly StatModifer _statModifer;
        
        public RegenerationHealthPassiveAbilityCreator(ICharacterHealthResolver healthResolver, StatModifer statModifer)
        {
            _healthResolver = healthResolver;
            _statModifer = statModifer;
        }
        
        public IPassiveAbility Create(PassiveAbilityData data)
        {
            return new RegenerationHealthPassiveAbility(_healthResolver.Health, _statModifer, data.Value, data.Cooldown);
        }
    }
}
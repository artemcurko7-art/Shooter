using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Creator
{
    [PassiveAbilityType(PassiveAbilityType.Temporary)]
    public class TemporaryPassiveAbilityCreator : IPassiveAbilityCreator
    {
        public IPassiveAbility Create(PassiveAbilityData data)
        {
            return new TemporaryPassiveAbility(data.StatType, data.Value, data.Cooldown);
        }
    }
}
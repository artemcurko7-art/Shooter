using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Creator
{
    [PassiveAbilityType(PassiveAbilityType.Constant)]
    public class ConstantPassiveAbilityCreator : IPassiveAbilityCreator
    {
        public IPassiveAbility Create(PassiveAbilityData data)
        {
            return new ConstantPassiveAbility(data.StatType, data.Value);
        }
    }
}
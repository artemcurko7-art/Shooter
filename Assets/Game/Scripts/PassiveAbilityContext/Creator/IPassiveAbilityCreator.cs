using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Passive;

namespace Game.Scripts.PassiveAbilityContext.Creator
{
    public interface IPassiveAbilityCreator
    {
        public IPassiveAbility Create(PassiveAbilityData data);
    }
}
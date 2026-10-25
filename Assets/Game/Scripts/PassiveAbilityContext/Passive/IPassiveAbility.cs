using Game.Scripts.Equipment.CharacterContext;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public interface IPassiveAbility
    {
        public void Apply(Character character);
    }
}
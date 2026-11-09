using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public class ConstantPassiveAbility : IPassiveAbility
    {
        private readonly StatType _type;
        private readonly int _value;
        
        public event Action<ICharacter, PassiveAbilityData> Applied;

        public ConstantPassiveAbility(StatType type, int value)
        {
            _type = type;
            _value = value;
        }

        public void Apply(ICharacter character, PassiveAbilityData passiveAbilityData)
        {
            character.Stats[_type].Increase(_value);
            
            Applied?.Invoke(character, passiveAbilityData);
        }
    }
}
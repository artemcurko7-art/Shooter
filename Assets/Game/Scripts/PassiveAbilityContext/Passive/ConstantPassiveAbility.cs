using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    [PassiveAbilityType(PassiveAbilityType.Constant)]
    public class ConstantPassiveAbility : IPassiveAbility
    {
        private readonly StatType _type;
        private readonly int _value;
        
        public event Action<ICharacter> Applied;

        public ConstantPassiveAbility(StatType type, int value)
        {
            _type = type;
            _value = value;
        }

        public void Apply(ICharacter character)
        {
            character.Stats[_type].Increase(_value);
            
            Applied?.Invoke(character);
            
            Debug.Log($"Constant");
        }
    }
}
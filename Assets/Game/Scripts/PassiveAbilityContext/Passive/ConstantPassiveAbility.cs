using System;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public class ConstantPassiveAbility : IPassiveAbility
    {
        private readonly StatType _type;
        private readonly int _value;
        
        public event Action<IAttacker, PassiveAbilityData> Applied;

        public ConstantPassiveAbility(StatType type, int value)
        {
            _type = type;
            _value = value;
        }

        public void Apply(IAttacker attacker, PassiveAbilityData passiveAbilityData)
        {
            attacker.Stats[_type].Increase(_value);
            
            Applied?.Invoke(attacker, passiveAbilityData);
        }
    }
}
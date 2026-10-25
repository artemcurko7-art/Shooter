using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    [PassiveAbilityType(PassiveAbilityType.Regeneration)]
    public class RegenerationPassiveAbility : IPassiveAbility
    {
        private const float Value = 8;
        
        public void Apply(Character character)
        {
            Stat stat = character.Stats[StatType.Health];
            
            float calculationValue = stat.MaxValue - stat.Value;
            float currentValue = 0;
            
            if (calculationValue < Value)
                currentValue = Value - calculationValue;
            else
                currentValue = Value;
            
            if (stat.Value < stat.MaxValue)
                stat.Increase(Mathf.RoundToInt(currentValue));
            
            Debug.Log($"Regeneration");
        }
    }
}
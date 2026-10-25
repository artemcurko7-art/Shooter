using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    [PassiveAbilityType(PassiveAbilityType.DamageBuff)]
    public class DamageBuffPassiveAbility : IPassiveAbility
    {
        public void Apply(Character character)
        {
            character.Stats[StatType.Damage].Increase(50);

            Debug.Log($"Damage buff");
        }
    }
}
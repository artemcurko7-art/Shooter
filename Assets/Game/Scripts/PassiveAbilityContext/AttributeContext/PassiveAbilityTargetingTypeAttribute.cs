using System;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.AttributeContext
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PassiveAbilityTargetingTypeAttribute : Attribute
    {
        public PassiveAbilityTargetingTypeAttribute(PassiveAbilityTargetingType type)
        {
            Type = type;
        }
        
        public PassiveAbilityTargetingType Type { get; }
    }
}
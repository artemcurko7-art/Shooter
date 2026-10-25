using System;
using Game.Scripts.PassiveAbilityContext.Type;

namespace Game.Scripts.PassiveAbilityContext.AttributeContext
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PassiveAbilityTypeAttribute : Attribute
    {
        public PassiveAbilityTypeAttribute(PassiveAbilityType type)
        {
            Type = type;
        }
        
        public PassiveAbilityType Type { get; }
    }
}
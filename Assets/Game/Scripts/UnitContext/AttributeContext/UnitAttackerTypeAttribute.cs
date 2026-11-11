using System;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.AttributeContext
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UnitAttackerTypeAttribute : Attribute
    {
        public UnitAttackerTypeAttribute(UnitAttackerType type)
        {
            Type = type;
        }
        
        public UnitAttackerType Type { get; }
    }
}
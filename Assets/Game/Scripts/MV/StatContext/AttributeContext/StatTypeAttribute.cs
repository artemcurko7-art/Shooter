using System;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.AttributeContext
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StatTypeAttribute : Attribute
    {
        public StatTypeAttribute(StatType type)
        {
            Type = type;
        }
        
        public StatType Type { get; }
    }
}
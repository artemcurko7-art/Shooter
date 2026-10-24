using System;
using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    [StatType(StatType.Health)]
    public class Health : Stat, IHealth
    {
        public Health(int value, bool isPercentageValue) : base(value, isPercentageValue) { }

        public int MaxValue { get; private set; }

        public override void Increase(int value)
        {
            base.Increase(value);
            
            MaxValue = Value;
        }

        protected override StatType GetStatType()
        {
            return StatType.Health;
        }
    }
}
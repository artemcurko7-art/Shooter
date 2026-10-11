using System;
using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    [StatType(StatType.Health)]
    public class Health : Stat
    {
        public Health(int value, bool isPercentageValue) : base(value, isPercentageValue) { }

        protected override StatType GetStatType()
        {
            return StatType.Health;
        }
    }
}
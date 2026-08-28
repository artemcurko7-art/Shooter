using System;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    public class Health : Stat
    {
        public Health(float value, bool isPercentageValue) : base(value, isPercentageValue) { }

        protected override StatType GetStatType()
        {
            return StatType.Health;
        }
    }
}
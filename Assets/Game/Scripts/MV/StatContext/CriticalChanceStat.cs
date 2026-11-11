using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    [StatType(StatType.CriticalChance)]
    public class CriticalChanceStat : Stat
    {
        public CriticalChanceStat(int value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.CriticalChance;
        }
    }
}
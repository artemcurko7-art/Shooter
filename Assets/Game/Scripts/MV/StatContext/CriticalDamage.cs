using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    [StatType(StatType.CriticalDamage)]
    public class CriticalDamage : Stat
    {
        public CriticalDamage(int value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.CriticalDamage;
        }
    }
}
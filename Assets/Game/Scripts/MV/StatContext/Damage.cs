using Game.Scripts.MV.StatContext.AttributeContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    [StatType(StatType.Damage)]
    public class Damage : Stat
    {
        public Damage(int value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.Damage;
        }
    }
}
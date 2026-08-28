using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    public class CriticalDamage : Stat
    {
        public CriticalDamage(float value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.CriticalDamage;
        }
    }
}
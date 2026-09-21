using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    public class CriticalChance : Stat
    {
        public CriticalChance(int value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.CriticalChance;
        }
    }
}
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    public class Defence : Stat
    {
        public Defence(float value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.Defence;
        }
    }
}
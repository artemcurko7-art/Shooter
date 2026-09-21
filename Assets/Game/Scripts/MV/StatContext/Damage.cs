using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext
{
    public class Damage : Stat
    {
        public Damage(int value, bool isPercentageValue) : base(value, isPercentageValue) { }
        
        protected override StatType GetStatType()
        {
            return StatType.Attack;
        }
    }
}
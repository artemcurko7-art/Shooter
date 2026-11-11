using System.Collections.Generic;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Creator
{
    [UnitAttackerType(UnitAttackerType.Melee)]
    public class MeleeAttackerCreator : IAttackerCreator
    {
        public IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats)
        {
            return new MeleeAttacker(stats[StatType.Damage],stats[StatType.Distance],stats[StatType.Radius]);
        }
    }
}
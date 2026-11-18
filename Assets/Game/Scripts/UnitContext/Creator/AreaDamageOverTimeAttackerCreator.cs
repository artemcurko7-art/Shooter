using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Creator
{
    [UnitAttackerType(UnitAttackerType.AreaDamageOverTime)]
    public class AreaDamageOverTimeAttackerCreator : AttackerCreator
    {
        public AreaDamageOverTimeAttackerCreator(UnitConfig unitConfig) : base(unitConfig) { }

        public override IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats)
        {
            return new AreaDamageOverTimeAttacker(UnitConfig.LayerMask, stats[StatType.Damage], stats[StatType.Radius], 2); // test cooldown
        }
    }
}
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Creator
{
    [UnitAttackerType(UnitAttackerType.AreaDamage)]
    public class AreaDamageAttackerCreator : AttackerCreator
    {
        public AreaDamageAttackerCreator(UnitConfig unitConfig) : base(unitConfig) { }

        public override IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats)
        {
            return new AreaDamageAttacker(UnitConfig.LayerMask, stats[StatType.Damage], stats[StatType.Radius]);
        }
    }
}
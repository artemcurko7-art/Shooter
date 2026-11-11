using System.Collections.Generic;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;

namespace Game.Scripts.UnitContext.Creator
{
    public interface IAttackerCreator
    {
        public IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats);
    }
}
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;

namespace Game.Scripts.UnitContext.Creator
{
    public abstract class AttackerCreator : IAttackerCreator
    {
        public AttackerCreator(UnitConfig unitConfig)
        {
            UnitConfig = unitConfig;
        }
        
        protected UnitConfig UnitConfig { get; private set; }

        public abstract IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats);
    }
}
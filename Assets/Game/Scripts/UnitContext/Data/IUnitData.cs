using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Data
{
    public interface IUnitData
    {
        public IReadOnlyDictionary<UnitType, List<UnitConfig>> Units { get; }
    }
}
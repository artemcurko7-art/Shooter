using System.Collections.Generic;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Data
{
    public interface IUnitAttackerData
    {
        public IReadOnlyDictionary<UnitAttackerType, IUnitAttacker> UnitAttackers { get; }
    }
}
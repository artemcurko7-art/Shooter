using System.Collections.Generic;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.PoolMono.Repository.Unit
{
    public interface IUnitPoolRepository
    {
        public IReadOnlyDictionary<UnitType, List<UnitPool>> Pools { get; }
    }
}
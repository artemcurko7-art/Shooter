using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Repository
{
    public interface IStatTypeRegistry
    {
        public IReadOnlyDictionary<StatType, System.Type> Types { get; }
    }
}
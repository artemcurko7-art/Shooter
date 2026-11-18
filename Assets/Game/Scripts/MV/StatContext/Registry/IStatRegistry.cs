using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Repository
{
    public interface IStatRegistry
    {
        public IReadOnlyDictionary<StatType, System.Type> Types { get; }
    }
}
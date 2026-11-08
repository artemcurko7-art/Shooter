using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Modifer
{
    public interface IStatModifer
    {
        public IReadOnlyDictionary<StatType, int> Stats { get; }
    }
}
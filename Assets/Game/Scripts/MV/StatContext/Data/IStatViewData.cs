using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Data
{
    public interface IStatViewData
    {
        public IReadOnlyDictionary<StatType, StatViewConfig> Stats { get; }
    }
}
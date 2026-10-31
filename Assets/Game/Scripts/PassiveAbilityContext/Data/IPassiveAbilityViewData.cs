using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Data
{
    public interface IPassiveAbilityViewData
    {
        public IReadOnlyDictionary<StatType, PassiveAbilityViewConfig> Views { get; }
    }
}
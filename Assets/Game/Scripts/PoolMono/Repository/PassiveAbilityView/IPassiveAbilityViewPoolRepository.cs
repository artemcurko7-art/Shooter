using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PoolMono.Repository.PassiveAbilityView
{
    public interface IPassiveAbilityViewPoolRepository
    {
        public IReadOnlyDictionary<PassiveAbilityData, PassiveAbilityViewPool> Pools { get; }
    }
}
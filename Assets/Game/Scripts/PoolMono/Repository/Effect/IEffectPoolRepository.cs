using System.Collections.Generic;
using Game.Scripts.VFX.Type;

namespace Game.Scripts.PoolMono.Repository.Effect
{
    public interface IEffectPoolRepository
    {
        public IReadOnlyDictionary<EffectType, EffectPool> Pools { get; }
    }
}
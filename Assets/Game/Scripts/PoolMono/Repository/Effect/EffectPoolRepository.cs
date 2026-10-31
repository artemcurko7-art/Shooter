using System.Collections.Generic;
using Game.Scripts.VFX.Data;
using Game.Scripts.VFX.Type;

namespace Game.Scripts.PoolMono.Repository.Effect
{
    public class EffectPoolRepository : IEffectPoolRepository
    {
        private readonly Dictionary<EffectType, EffectPool> _pools = new();
        
        public EffectPoolRepository(IEffectData data, EffectPool[] pools)
        {
            foreach (var pool in pools)
            {
                pool.SetPrefab(data.Effects[pool.Type]);
                _pools.Add(pool.Type, pool);
            }
        }

        public IReadOnlyDictionary<EffectType, EffectPool> Pools => _pools;
    }
}
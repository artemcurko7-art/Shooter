using System.Collections.Generic;
using Game.Scripts.Extensions;
using Game.Scripts.VFX.Data;
using Game.Scripts.VFX.Type;
using Zenject;

namespace Game.Scripts.PoolMono.Repository.Effect
{
    public class EffectPoolRepository : IEffectPoolRepository
    {
        private readonly Dictionary<EffectType, EffectPool> _pools = new();
        
        public EffectPoolRepository(IEffectData data, DiContainer container)
        {
            foreach (var effect in data.Effects)
            {
                var pool = container.InstantiateEffectPool(effect.Value.View);
                _pools.Add(effect.Key, pool);
            }
        }

        public IReadOnlyDictionary<EffectType, EffectPool> Pools => _pools;
    }
}
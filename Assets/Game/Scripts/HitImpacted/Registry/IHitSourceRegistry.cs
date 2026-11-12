using System;
using System.Collections.Generic;
using Game.Scripts.HitImpacted.HitSources;

namespace Game.Scripts.HitImpacted.Registry
{
    public interface IHitSourceRegistry
    {
        public IEnumerable<IHitSource> GetSources(Func<IHitSource, bool> filter = null);
    }
}
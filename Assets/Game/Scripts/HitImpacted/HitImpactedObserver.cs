using System.Collections.Generic;
using Game.Scripts.HitImpacted.HitSources;
using Game.Scripts.HitImpacted.Registry;

namespace Game.Scripts.HitImpacted
{
    public abstract class HitImpactedObserver : RemovedHitImpactedObserver
    {
        protected HitImpactedObserver(IHitSourceRegistry hitSourceRegistry) : base(hitSourceRegistry) { }

        protected override IEnumerable<IHitSource> GetHitSources()
        {
            return HitSourceRegistry.GetSources();
        }
    }
}
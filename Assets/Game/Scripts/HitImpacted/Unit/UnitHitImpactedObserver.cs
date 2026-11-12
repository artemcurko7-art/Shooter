using System.Collections.Generic;
using Game.Scripts.HitImpacted.HitSources;
using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.Service.UnitContext;

namespace Game.Scripts.HitImpacted.Unit
{
    public abstract class UnitHitImpactedObserver : AddedHitImpactedObserver
    {
        protected UnitHitImpactedObserver(IHitSourceRegistry hitSourceRegistry) : base(hitSourceRegistry) { }

        protected override IEnumerable<IHitSource> GetHitSources()
        { 
            return HitSourceRegistry.GetSources(sources => sources is IUnitAttackerService);
        }
    }
}
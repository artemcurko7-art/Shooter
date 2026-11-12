using System.Collections.Generic;
using Game.Scripts.HitImpacted.HitSources;
using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.PoolMono;

namespace Game.Scripts.HitImpacted.Character
{
    public abstract class CharacterHitImpactedObserver : RemovedHitImpactedObserver
    {
        protected CharacterHitImpactedObserver(IHitSourceRegistry hitSourceRegistry) : base(hitSourceRegistry) { }

        protected override IEnumerable<IHitSource> GetHitSources()
        {
            return HitSourceRegistry.GetSources(sources => sources is ProjectilePool);
        }
    }
}
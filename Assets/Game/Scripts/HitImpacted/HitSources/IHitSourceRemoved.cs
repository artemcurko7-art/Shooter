using System;

namespace Game.Scripts.HitImpacted.HitSources
{
    public interface IHitSourceRemoved : IHitSource
    {
        public event Action<IImpactReceiver> Removed;
    }
}
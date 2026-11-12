using System;

namespace Game.Scripts.HitImpacted.HitSources
{
    public interface IHitSource
    {
        public event Action<IImpactReceiver> Added;
    }
}
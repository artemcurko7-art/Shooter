using System;

namespace Game.Scripts.HitImpacted
{
    public interface IImpactReceiver
    {
        public event Action<HitContext> HitImpacted;
    }
}
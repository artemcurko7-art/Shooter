using System.Collections.Generic;
using Game.Scripts.HitImpacted.Registry;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.HitImpacted.HitSources
{
    public abstract class AddedHitImpactedObserver : ISubscriber
    {
        private readonly List<IImpactReceiver> _listeners = new();

        public AddedHitImpactedObserver(IHitSourceRegistry hitSourceRegistry)
        {
            HitSourceRegistry = hitSourceRegistry;
        }
        
        protected IHitSourceRegistry HitSourceRegistry { get; private set; }
        
        public virtual void Subscribe()
        {
            foreach (var hitSource in GetHitSources())
                hitSource.Added += OnAdded;
        }

        public virtual void Unsubscribe()
        {
            foreach (var hitSource in GetHitSources())
                hitSource.Added -= OnAdded;
            
            foreach (var listener in _listeners)
                listener.HitImpacted -= OnHitImpacted;
            
            _listeners.Clear();
        }
        
        protected abstract IEnumerable<IHitSource> GetHitSources();
        protected abstract void OnHitImpacted(HitContext hitContext);
        
        private void OnAdded(IImpactReceiver receiver)
        {
            receiver.HitImpacted += OnHitImpacted;
            _listeners.Add(receiver);
        }
    }
}
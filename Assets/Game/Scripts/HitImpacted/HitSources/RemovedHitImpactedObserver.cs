using Game.Scripts.HitImpacted.Registry;

namespace Game.Scripts.HitImpacted.HitSources
{
    public abstract class RemovedHitImpactedObserver : AddedHitImpactedObserver
    {
        protected RemovedHitImpactedObserver(IHitSourceRegistry hitSourceRegistry) : base(hitSourceRegistry) { }

        public override void Subscribe()
        {
            base.Subscribe();
            
            foreach (var source in GetHitSources())
                if (source is IHitSourceRemoved removedSource)
                    removedSource.Removed += OnRemoved;
        }
        
        public override void Unsubscribe()
        {
            base.Unsubscribe();

            foreach (var source in GetHitSources())
                if (source is IHitSourceRemoved removedSource)
                    removedSource.Removed -= OnRemoved;
        }

        private void OnRemoved(IImpactReceiver receiver)
        {
            receiver.HitImpacted -= OnHitImpacted;
        }
    }
}
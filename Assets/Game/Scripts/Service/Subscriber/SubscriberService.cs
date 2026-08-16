using UnityEngine;
using Zenject;

namespace Game.Scripts.Service.Subscriber
{
    public class SubscriberService : MonoBehaviour
    {
        private ISubscriber[] _subscribers;
    
        [Inject]
        public void Construct(ISubscriber[] subscribers)
        {
            _subscribers = subscribers;
        
            Initialize();
        }

        private void Initialize()
        {
            foreach (var subscriber in _subscribers)
                subscriber.Subscribe();
        }

        private void OnDestroy()
        {
            foreach (var subscriber in _subscribers)
                subscriber.Unsubscribe();
        }
    }
}
using System;
using UnityEngine;
using YG;
using Zenject;

namespace Game.Scripts.Service.Subscriber
{
    public class SubscriberService : MonoBehaviour
    {
        private ISubscriber[] _subscribers;

        public event Action Subscribed;

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

            Subscribed?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (var subscriber in _subscribers)
                subscriber.Unsubscribe();
        }
    }
}
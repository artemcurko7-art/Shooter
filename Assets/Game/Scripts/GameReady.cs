using Game.Scripts.Service.Subscriber;
using UnityEngine;
using YG;

namespace Game.Scripts
{
    public class GameReady : MonoBehaviour
    {
        [SerializeField] private SubscriberService _subscriberService;
        
        private bool _isGameReadySent;

        private void OnEnable()
        {
            _subscriberService.Subscribed += SendGameReady;
        }

        private void OnDisable()
        {
            _subscriberService.Subscribed -= SendGameReady;
        }

        private void SendGameReady()
        {
            if (_isGameReadySent)
                return;

            _isGameReadySent = true;

            YG2.GameReadyAPI();
            YG2.GameplayStart();
        }
    }
}
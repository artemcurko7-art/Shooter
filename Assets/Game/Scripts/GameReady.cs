using Game.Scripts.Service.Subscriber;
using UnityEngine;
using YG;
using Zenject;

namespace Game.Scripts
{
    public class GameReady : MonoBehaviour
    {
        private bool _isGameReadySent;

        [Inject]
        public void Construct()
        {
            if (_isGameReadySent)
                return;

            _isGameReadySent = true;

            YG2.GameReadyAPI();
            YG2.GameplayStart();
        }
    }
}
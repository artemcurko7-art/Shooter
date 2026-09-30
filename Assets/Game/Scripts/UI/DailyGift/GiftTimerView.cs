using UnityEngine;

namespace Game.Scripts.UI.DailyGift
{
    public class GiftTimerView : MonoBehaviour
    {
        [SerializeField] private TimeTracker _timeTracker;

        private void OnEnable()
        {
            _timeTracker.StartTracking();
        }

        private void OnDisable()
        {
            _timeTracker.StopTracking();
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.DailyGift
{
    public class TimeTracker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeToGiftText;

        private GiftTimer _giftTimer;
        private Coroutine _timerCoroutine;

        private void Awake()
        {
            _giftTimer = new GiftTimer();
        }

        public void StartTracking()
        {
            if (_timerCoroutine != null)
                return;

            UpdateTime();
            _timerCoroutine = StartCoroutine(UpdateTimer());
        }

        public void StopTracking()
        {
            if (_timerCoroutine == null)
                return;

            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }

        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                UpdateTime();
            }
        }

        private void UpdateTime()
        {
            if (_giftTimer == null) return;
            var remainingSeconds = _giftTimer.GetRemainingSeconds();

            _timeToGiftText.text = FormatTime(remainingSeconds);
        }

        private static string FormatTime(long totalSeconds)
        {
            var hours = totalSeconds / 3600;
            var minutes = totalSeconds % 3600 / 60;
            var seconds = totalSeconds % 60;

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }
}
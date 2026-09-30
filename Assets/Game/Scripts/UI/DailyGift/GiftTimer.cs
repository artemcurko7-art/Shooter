using System;
using YG;

namespace Game.Scripts.UI.DailyGift
{
    public class GiftTimer
    {
        public bool IsRunning => GetRemainingSeconds() > 0;

        public bool IsReady => GetRemainingSeconds() <= 0;

        public void Start(TimeSpan duration)
        {
            YG2.saves.GiftEndTime =
                TimeProvider.UnixNowSeconds + (long)duration.TotalSeconds;

            YG2.SaveProgress();
        }

        public void StartHours(int hours)
        {
            Start(TimeSpan.FromHours(hours));
        }

        public void StartMinutes(int minutes)
        {
            Start(TimeSpan.FromMinutes(minutes));
        }

        public void StartSeconds(int seconds)
        {
            Start(TimeSpan.FromSeconds(seconds));
        }

        public long GetRemainingSeconds()
        {
            var endTime = YG2.saves.GiftEndTime;

            if (endTime <= 0)
                return TimeProvider.SecondsUntilNextDay();

            var remaining = endTime - TimeProvider.UnixNowSeconds;

            return Math.Max(0, remaining);
        }

        public void Reset()
        {
            YG2.saves.GiftEndTime = 0;

            YG2.SaveProgress();
        }
    }
}
using System;
using YG;

namespace Game.Scripts.UI.DailyGift
{
    public static class TimeProvider
    {
        public static long UnixNowMilliseconds => YG2.ServerTime();

        public static long UnixNowSeconds => UnixNowMilliseconds / 1000;

        public static long SecondsUntilNextDay()
        {
            var now = DateTimeOffset.FromUnixTimeSeconds(UnixNowSeconds);
            var nextDay = now.Date.AddDays(1);

            return (long)(nextDay - now).TotalSeconds;
        }
    }
}
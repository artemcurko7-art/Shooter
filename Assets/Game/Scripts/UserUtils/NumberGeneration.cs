using System;

namespace Game.Scripts.UserUtils
{
    public static class NumberGeneration
    {
        private static readonly Random s_random = new();

        public static int GetRandom(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue + 1);
        }
    }
}
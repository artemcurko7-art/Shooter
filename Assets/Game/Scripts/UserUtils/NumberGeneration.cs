using System;

namespace Game.Scripts.UserUtils
{
    public static class NumberGeneration
    {
        private static readonly Random s_random = new();

        public static int GetIntegerRandom(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue + 1);
        }

        public static float GetFloatRandom(float minValue, float maxValue)
        {
            return minValue + (float)s_random.NextDouble() * (maxValue - minValue);
        }
    }
}
using System;
using System.Globalization;

namespace Game.Scripts.UserUtils
{
    public static class NumberFormatter
    {
        private static readonly string[] _suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
        
        public static string Format(double value, int decimals = 1)
        {
            if (value < 0)
                return "-" + Format(-value, decimals);

            if (value < 1000)
                return Math.Floor(value).ToString(CultureInfo.InvariantCulture);
            
            int suffixIndex = 0;
            while (value >= 1000 && suffixIndex < _suffixes.Length - 1)
            {
                value /= 1000;
                suffixIndex++;
            }
            
            string format = "0." + new string('#', decimals);
            
            return value.ToString(format, CultureInfo.InvariantCulture) + _suffixes[suffixIndex];
        }
    }
}
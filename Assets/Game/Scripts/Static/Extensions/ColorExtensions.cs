using UnityEngine;

namespace Game.Scripts.Static.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
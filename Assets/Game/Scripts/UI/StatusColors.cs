using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public static class StatusColors
    {
        private const float ColorMultiplier = 1f;
        private const float FadeDuration = 0.3f;
        
        private static readonly Color Positive = new Color(0, 0.7f, 0.45f).WithAlpha(100);
        private static readonly Color Negative = new Color(0.8f, 0.2f, 0.2f).WithAlpha(100);

        private static readonly Color PositiveHighlighted = new Color(0, 0.6f, 0.4f).WithAlpha(100);
        private static readonly Color NegativeHighlighted = new Color(0.8f, 0.1f, 0.15f).WithAlpha(100);

        public static Color GetColor(bool isPositive)
        {
            return isPositive ? Positive : Negative;
        }

        public static ColorBlock GetColorBlock(bool isPositive)
        {
            ColorBlock colorBlock = default;
            colorBlock.colorMultiplier = ColorMultiplier;
            colorBlock.fadeDuration = FadeDuration;
            
            colorBlock.normalColor = isPositive ? Positive : Negative;
            colorBlock.highlightedColor = isPositive ? PositiveHighlighted : NegativeHighlighted;
            colorBlock.pressedColor = isPositive ? PositiveHighlighted : NegativeHighlighted;
            colorBlock.selectedColor = isPositive ? Positive : Negative;
            colorBlock.disabledColor = isPositive ? Positive : Negative;
            
            return colorBlock;
        }
    }
}
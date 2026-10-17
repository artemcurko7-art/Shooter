using UnityEngine;
using YG;

namespace Game.Scripts.UI
{
    public enum RarityType
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public static class RarityUtils
    {
        public static string GetLocalizedName(RarityType rarity)
        {
            var lang = YG2.lang; // твой язык

            return lang switch
            {
                "ru" => rarity switch
                {
                    RarityType.Common => "Обычный",
                    RarityType.Rare => "Редкий",
                    RarityType.Epic => "Эпичный",
                    RarityType.Legendary => "Легендарный",
                    _ => "Неизвестный"
                },
                "en" => rarity switch
                {
                    RarityType.Common => "Common",
                    RarityType.Rare => "Rare",
                    RarityType.Epic => "Epic",
                    RarityType.Legendary => "Legendary",
                    _ => "Unknown"
                },
                "tr" => rarity switch
                {
                    RarityType.Common => "Sıradan",
                    RarityType.Rare => "Nadir",
                    RarityType.Epic => "Epik",
                    RarityType.Legendary => "Efsanevi",
                    _ => "Bilinmeyen"
                },
                _ => rarity.ToString()
            };
        }


        public static Color GetColor(RarityType rarity)
        {
            return rarity switch
            {
                RarityType.Common => new Color(0.8f, 0.8f, 0.8f),
                RarityType.Rare => new Color(0f, 0.3f, 1f),
                RarityType.Epic => new Color(1f, 0.13f, 0.08f),
                RarityType.Legendary => new Color(1f, 0.9f, 0f),
                _ => Color.white
            };
        }

        public static float GetSliderValue(RarityType rarity)
        {
            return rarity switch
            {
                RarityType.Common => 0.0f,
                RarityType.Rare => 1.0f / 3.0f,
                RarityType.Epic => 2.0f / 3.0f,
                RarityType.Legendary => 1.0f,
                _ => 0.0f
            };
        }
    }
}
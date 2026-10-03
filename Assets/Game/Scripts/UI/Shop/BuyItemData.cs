using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.Shop
{
    [CreateAssetMenu(fileName = "BuyItemData", menuName = "Data/BuyItemData")]
    public class BuyItemData : ScriptableObject
    {
        public List<BuyItem> items = new();

        [Serializable]
        public class BuyItem
        {
            public LocalizedText nameTextTranslations;
            public ResourceType resource;
            public Sprite icon;
            public int count;
            public float price;

            [Serializable]
            public class LocalizedText
            {
                public string ru;
                public string en;
                public string tr;
            }

            public string GetLocalizedName(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => nameTextTranslations.ru,
                    "en" => nameTextTranslations.en,
                    "tr" => nameTextTranslations.tr,
                    _ => nameTextTranslations.en,
                };
            }
        }
    }
}
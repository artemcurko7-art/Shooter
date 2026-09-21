using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Gifts
{
    public class GiftData : MonoBehaviour
    {
        public List<Gift> Gifts = new();

        [Serializable]
        public class Gift
        {
            public LocalizedText GiftTextTranslations;
            public Sprite icon;
            public int count;

            [Serializable]
            public class LocalizedText
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            public string GetLocalizedGift(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => GiftTextTranslations.Ru,
                    "en" => GiftTextTranslations.En,
                    "tr" => GiftTextTranslations.Tr,
                    _ => GiftTextTranslations.En,
                };
            }
        }
    }
}
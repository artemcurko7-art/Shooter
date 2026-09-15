using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Data/MapData")]
    public class MapData : ScriptableObject
    {
        public List<Map> Maps = new();

        [Serializable]
        public class Map
        {
            public LocalizedName MapNameTranslations;
            public Sprite icon;
            public int difficulty;
            public bool hasBoss;

            [Serializable]
            public class LocalizedName
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            public string GetLocalizedName(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => MapNameTranslations.Ru,
                    "en" => MapNameTranslations.En,
                    "tr" => MapNameTranslations.Tr,
                    _ => MapNameTranslations.En,
                };
            }
        }
    }
}
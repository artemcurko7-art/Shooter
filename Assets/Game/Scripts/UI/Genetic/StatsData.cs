using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Genetic
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Data/StatsData")]
    public class StatsData : ScriptableObject
    {
        public List<Stat> Stats = new();

        [Serializable]
        public class Stat
        {
            [Header("Основные данные")]
            public int id;
            public string name;
            public Sprite icon;
            public int value;

            [Header("Локализация")]
            public LocalizedName StatNameTranslations = new();

            [Serializable]
            public class LocalizedName
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            public string GetLocalizedName(string languageCode)
            {
                if (StatNameTranslations == null)
                    return name ?? string.Empty;

                return languageCode switch
                {
                    "ru" => GetTranslation(StatNameTranslations.Ru),
                    "tr" => GetTranslation(StatNameTranslations.Tr),
                    "en" => GetTranslation(StatNameTranslations.En),
                    _ => GetTranslation(StatNameTranslations.En)
                };
            }

            private string GetTranslation(string translation)
            {
                if (!string.IsNullOrEmpty(translation))
                    return translation;

                return name ?? string.Empty;
            }
        }
    }
}
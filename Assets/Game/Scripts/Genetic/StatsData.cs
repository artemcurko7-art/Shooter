using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Genetic
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Genetic/StatsData")]
    public class StatsData : ScriptableObject
    {
        public List<Stat> Stats = new ();

        [Serializable]
        public class Stat
        {
            public string name;
            public Sprite icon;
            public int value;

            public Stat(string name, Sprite icon, int value, LocalizedName statNameTranslations)
            {
                this.name = name;
                this.icon = icon;
                this.value = value;
                StatNameTranslations = statNameTranslations;
            }

            public LocalizedName StatNameTranslations;

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
                    "ru" => StatNameTranslations.Ru,
                    "en" => StatNameTranslations.En,
                    "tr" => StatNameTranslations.Tr,
                    _ => StatNameTranslations.En,
                };
            }
        }
    }
}
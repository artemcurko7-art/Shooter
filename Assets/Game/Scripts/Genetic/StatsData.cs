using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Character.Genetic
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Genetic/StatsData")]
    public class StatsData : ScriptableObject
    {
        public List<Stat> Stats = new List<Stat>();

        [Serializable]
        public class Stat
        {
            public string name;
            public Sprite icon;

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
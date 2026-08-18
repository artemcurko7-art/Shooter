using System;
using System.Collections.Generic;
using UnityEngine;

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
            public int maxCount;

            public LocalizedName SkinNameTranslations;

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
                    "ru" => SkinNameTranslations.Ru,
                    "en" => SkinNameTranslations.En,
                    "tr" => SkinNameTranslations.Tr,
                    _ => SkinNameTranslations.En,
                };
            }
        }
    }
}
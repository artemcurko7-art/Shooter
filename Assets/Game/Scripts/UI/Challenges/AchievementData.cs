using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Challenges
{
    [CreateAssetMenu(fileName = "AchieveData", menuName = "Data/AchieveData")]
    public class AchievementData : ScriptableObject
    {
        public List<Achieve> Achieves = new();

        [Serializable]
        public class Achieve
        {
            public Sprite icon;
            public LocalizedName AchieveNameTranslations;
            public LocalizedDescription AchieveDescriptionTranslations;
            public bool isOpened;

            [Serializable]
            public class LocalizedName
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            [Serializable]
            public class LocalizedDescription
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            public string GetLocalizedName(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => AchieveNameTranslations.Ru,
                    "en" => AchieveNameTranslations.En,
                    "tr" => AchieveNameTranslations.Tr,
                    _ => AchieveNameTranslations.En,
                };
            }

            public string GetLocalizedDescription(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => AchieveDescriptionTranslations.Ru,
                    "en" => AchieveDescriptionTranslations.En,
                    "tr" => AchieveDescriptionTranslations.Tr,
                    _ => AchieveDescriptionTranslations.En,
                };
            }
        }
    }
}
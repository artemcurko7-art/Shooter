using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.WheelFortune
{
    [CreateAssetMenu(fileName = "RewardData", menuName = "Data/RewardData")]
    public class RewardData : ScriptableObject
    {
        public List<Reward> Rewards = new();

        [Serializable]
        public class Reward
        {
            public Sprite icon;
            public Sprite background;
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
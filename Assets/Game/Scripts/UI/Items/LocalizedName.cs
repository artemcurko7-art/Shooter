using System;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [Serializable]
    public class LocalizedName
    {
        public string Ru;
        public string En;
        public string Tr;

        public string Get(string languageCode)
        {
            return languageCode switch
            {
                "ru" => Ru,
                "en" => En,
                "tr" => Tr,
                _ => En
            };
        }
    }
}
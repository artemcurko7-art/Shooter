using UnityEngine;
using YG;

namespace Game.Scripts.UI
{
    public static class Localization
    {
        public static string GetLocalizedBuyText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Улучшить",
                "en" => "Improve",
                "tr" => "İyileştirmek",
                _ => "Improve!",
            };
        }

        public static string GetGeneticTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Генетика",
                "en" => "Genetics",
                "tr" => "Genetik",
                _ => "Improve!",
            };
        }

        public static string GetSpinButtonText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Крутить!",
                "en" => "Spin!",
                "tr" => "Döndürmek!",
                _ => "Spin!",
            };
        }
        
        public static string GetNotAvailableButtonText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Недоступно",
                "en" => "Unavailable",
                "tr" => "Mevcut değil",
                _ => "Unavailable",
            };
        }
    }
}

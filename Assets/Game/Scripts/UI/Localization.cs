using Game.Scripts.UI.Items;
using YG;

namespace Game.Scripts.UI
{
    public static class Localization
    {
        public static string GetUpgradeText()
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

        public static string GetBuyText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Купить",
                "en" => "Buy",
                "tr" => "satın al",
                _ => "Buy",
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

        public static string GetDailyGiftsTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "След. подарок через:",
                "en" => "The next gift:",
                "tr" => "Bir sonraki hediye",
                _ => "The next gift:",
            };
        }

        public static string GetNameTranslations(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Offer => Localization.GetOfferTitleText(),
                ResourceType.Gem => Localization.GetGemTitleText(),
                ResourceType.Gold => Localization.GetGoldTitleText(),
                ResourceType.Energy => Localization.GetEnergyTitleText(),
                ResourceType.LootBox => Localization.GetLootBoxTitleText(),
                ResourceType.LootKey => Localization.GetLootKeyTitleText(),
                ResourceType.Skin => Localization.GetSkinsTitleText(),
                _ => "Wrong resource type: Error."
            };
        }

        private static string GetOfferTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Предложение",
                "en" => "Offer",
                "tr" => "Teklif",
                _ => "Offer",
            };
        }

        private static string GetGoldTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Золото",
                "en" => "Gold",
                "tr" => "altın",
                _ => "gold",
            };
        }

        private static string GetGemTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Гемы",
                "en" => "Gems",
                "tr" => "Bir sonraki hediye",
                _ => "TTaşlar",
            };
        }

        private static string GetEnergyTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Заряд",
                "en" => "Energy",
                "tr" => "Yük",
                _ => "Energy",
            };
        }

        private static string GetLootBoxTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Коробка",
                "en" => "Loot box",
                "tr" => "yağma kutusu",
                _ => "Loot box",
            };
        }

        private static string GetLootKeyTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Ключ",
                "en" => "Loor key",
                "tr" => "yağma anahtarı",
                _ => "Loot key",
            };
        }

        private static string GetSkinsTitleText()
        {
            var languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Скин",
                "en" => "Skin",
                "tr" => "Cilt",
                _ => "Skin",
            };
        }
    }
}
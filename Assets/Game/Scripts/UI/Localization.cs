using Game.Scripts.UI.Items;
using YG;

namespace Game.Scripts.UI
{
    public static class Localization
    {
        public static string GetUpgradeText()
        {
            return YG2.lang switch
            {
                "ru" => "Улучшить",
                "en" => "Upgrade",
                "tr" => "Yükselt",
                _ => "Upgrade"
            };
        }

        public static string GetBuyText()
        {
            return YG2.lang switch
            {
                "ru" => "Купить",
                "en" => "Buy",
                "tr" => "Satın al",
                _ => "Buy"
            };
        }

        public static string GetGeneticTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Генетика",
                "en" => "Genetics",
                "tr" => "Genetik",
                _ => "Genetics"
            };
        }

        public static string GetSpinButtonText()
        {
            return YG2.lang switch
            {
                "ru" => "Крутить!",
                "en" => "Spin!",
                "tr" => "Çevir!",
                _ => "Spin!"
            };
        }

        public static string GetAvailableButtonText()
        {
            return YG2.lang switch
            {
                "ru" => "Доступно",
                "en" => "Available",
                "tr" => "Mevcut",
                _ => "Available"
            };
        }

        public static string GetNotAvailableButtonText()
        {
            return YG2.lang switch
            {
                "ru" => "Недоступно",
                "en" => "Unavailable",
                "tr" => "Mevcut değil",
                _ => "Unavailable"
            };
        }

        public static string GetDailyGiftsTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "След. подарок через:",
                "en" => "Next gift in:",
                "tr" => "Sonraki hediyeye:",
                _ => "Next gift in:"
            };
        }

        public static string GetNameTranslations(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Offer => GetOfferTitleText(),
                ItemType.Gem => GetGemTitleText(),
                ItemType.Gold => GetGoldTitleText(),
                ItemType.Energy => GetEnergyTitleText(),
                ItemType.LootBox => GetLootBoxTitleText(),
                ItemType.LootKey => GetLootKeyTitleText(),
                ItemType.Skin => GetSkinsTitleText(),
                _ => string.Empty
            };
        }

        private static string GetOfferTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Предложение",
                "en" => "Offer",
                "tr" => "Teklif",
                _ => "Offer"
            };
        }

        private static string GetGoldTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Золото",
                "en" => "Gold",
                "tr" => "Altın",
                _ => "Gold"
            };
        }

        private static string GetGemTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Гемы",
                "en" => "Gems",
                "tr" => "Mücevherler",
                _ => "Gems"
            };
        }

        private static string GetEnergyTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Энергия",
                "en" => "Energy",
                "tr" => "Enerji",
                _ => "Energy"
            };
        }

        private static string GetLootBoxTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Коробка",
                "en" => "Loot box",
                "tr" => "Ganimet kutusu",
                _ => "Loot box"
            };
        }

        private static string GetLootKeyTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Ключ",
                "en" => "Loot key",
                "tr" => "Ganimet anahtarı",
                _ => "Loot key"
            };
        }

        private static string GetSkinsTitleText()
        {
            return YG2.lang switch
            {
                "ru" => "Скин",
                "en" => "Skin",
                "tr" => "Kostüm",
                _ => "Skin"
            };
        }
    }
}
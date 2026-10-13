using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.UI.Items;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Shop
{
    [CreateAssetMenu(fileName = "BuyItemData", menuName = "Game/Shop/Buy Item Data")]
    public class BuyItemData : ScriptableObject
    {
        [SerializeField] private List<BuyItem> _items = new();

        public List<BuyItem> Items => _items;

        [Serializable]
        public class BuyItem
        {
            public Reward Reward;
            public OfferDefinition Offer;
            public float Price;

            public bool IsOffer => Offer != null;

            public float GetPrice()
            {
                return IsOffer ? Offer.Price : Price;
            }

            public Sprite GetIcon()
            {
                if (!IsOffer) return Reward?.Icon;
                
                if (Offer.Rewards == null || Offer.Rewards.Count == 0)
                    return null;

                return Offer.Rewards[0]?.Icon;

            }

            public int GetCount()
            {
                return IsOffer ? 1 : Reward?.Count ?? 0;
            }

            public string GetLocalizedName()
            {
                if (IsOffer)
                    return Localization.GetNameTranslations(ItemType.Offer);

                if (Reward == null)
                    return string.Empty;

                return Reward.Type switch
                {
                    RewardType.Item when Reward.Item != null =>
                        Localization.GetNameTranslations(Reward.Item.Type),

                    RewardType.Skin when Reward.Skin != null =>
                        Reward.Skin.GetLocalizedName(YG2.lang),

                    _ => string.Empty
                };
            }

            public string GetLocalizedTypeName()
            {
                if (IsOffer)
                    return Localization.GetNameTranslations(ItemType.Offer);

                if (Reward == null)
                    return string.Empty;

                return Reward.Type switch
                {
                    RewardType.Item when Reward.Item != null =>
                        Localization.GetNameTranslations(Reward.Item.Type),

                    RewardType.Skin =>
                        Localization.GetNameTranslations(ItemType.Skin),

                    _ => string.Empty
                };
            }

            public bool IsValid()
            {
                if (IsOffer)
                    return true;

                if (Reward == null)
                    return false;

                return Reward.Type switch
                {
                    RewardType.Item => Reward.Item != null,
                    RewardType.Skin => Reward.Skin != null,
                    _ => false
                };
            }
        }
    }
}
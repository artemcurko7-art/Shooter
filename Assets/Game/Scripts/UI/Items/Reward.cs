using System;
using Game.Scripts.UI;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Items
{
    public enum RewardType
    {
        Item,
        Skin
    }

    [Serializable]
    public class Reward
    {
        [SerializeField] private RewardType _type;
        [SerializeField] private ItemDefinition _item;
        [SerializeField] private SkinDefinition _skin;
        [SerializeField, Min(1)] private int _count = 1;

        public RewardType Type => _type;
        public ItemDefinition Item => _item;
        public SkinDefinition Skin => _skin;
        public int Count => _count;

        public string Id => _type switch
        {
            RewardType.Item => _item ? _item.Id : string.Empty,
            RewardType.Skin => _skin ? _skin.Id : string.Empty,
            _ => string.Empty
        };

        public Sprite Icon => _type switch
        {
            RewardType.Item => _item ? _item.GetIcon(_count) : null,
            RewardType.Skin => _skin ? _skin.Icon : null,
            _ => null
        };

        public string GetLocalizedName()
        {
            return _type switch
            {
                RewardType.Item => _item ? _item.GetLocalizedName() : string.Empty,
                RewardType.Skin => _skin ? _skin.GetLocalizedName(YG2.lang) : string.Empty,
                _ => string.Empty
            };
        }

        public string GetLocalizedTypeName()
        {
            return _type switch
            {
                RewardType.Item when _item =>
                    Localization.GetNameTranslations(_item.Type),

                RewardType.Skin =>
                    Localization.GetNameTranslations(ItemType.Skin),

                _ => string.Empty
            };
        }
    }
}
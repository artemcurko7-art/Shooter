using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.Shop
{
    [CreateAssetMenu(fileName = "BuyItemData", menuName = "Game/Shop/Buy Item Data")]
    public class BuyItemData : ScriptableObject
    {
        [Header("Общее определение")]
        [SerializeField] private bool _useSharedDefinition;
        [SerializeField] private RewardDefinition _sharedDefinition;

        [Header("Предметы магазина")]
        [SerializeField] private List<BuyItem> _items = new();

        public List<BuyItem> Items => _items;

        public RewardDefinition GetDefinition(BuyItem buyItem)
        {
            if (buyItem == null)
                return null;

            return _useSharedDefinition ? _sharedDefinition : buyItem.Definition;
        }

        [Serializable]
        public class BuyItem
        {
            public RewardDefinition Definition;
            [Min(1)] public int Count = 1;
            public float Price;

            public bool IsOffer => Definition is OfferDefinition;

            public bool IsOfferFor(RewardDefinition definition)
            {
                return definition is OfferDefinition;
            }

            public Sprite GetIcon()
            {
                return GetIcon(Definition);
            }

            public Sprite GetIcon(RewardDefinition definition)
            {
                return definition ? definition.GetIcon(GetCount(definition)) : null;
            }

            public int GetCount()
            {
                return GetCount(Definition);
            }

            public int GetCount(RewardDefinition definition)
            {
                return IsOfferFor(definition) ? 1 : Mathf.Max(1, Count);
            }

            public string GetLocalizedName()
            {
                return GetLocalizedName(Definition);
            }

            public string GetLocalizedName(RewardDefinition definition)
            {
                return definition ? definition.GetLocalizedName() : string.Empty;
            }

            public string GetLocalizedTypeName()
            {
                return GetLocalizedTypeName(Definition);
            }

            public string GetLocalizedTypeName(RewardDefinition definition)
            {
                return definition ? definition.GetLocalizedTypeName() : string.Empty;
            }

            public bool IsValid()
            {
                return Definition != null;
            }

            public bool IsValid(RewardDefinition definition)
            {
                return definition != null;
            }
        }
    }
}
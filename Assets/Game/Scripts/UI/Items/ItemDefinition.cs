using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game/Items/Item")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private ItemType _type;
        [SerializeField] private List<IconVariant> _iconVariants = new();

        public string Id => _id;
        public ItemType Type => _type;

        public string GetLocalizedName()
        {
            return Localization.GetNameTranslations(_type);
        }

        public Sprite GetIcon(int count)
        {
            if (_iconVariants == null || _iconVariants.Count == 0)
                return null;

            Sprite fallback = null;
            Sprite result = null;
            var bestThreshold = int.MinValue;

            foreach (var variant in _iconVariants)
            {
                if (variant == null || variant.Icon == null)
                    continue;

                if (fallback == null)
                    fallback = variant.Icon;

                if (count < variant.MinCount || variant.MinCount <= bestThreshold)
                    continue;

                result = variant.Icon;
                bestThreshold = variant.MinCount;
            }

            return result != null ? result : fallback;
        }

        [Serializable]
        private class IconVariant
        {
            [Min(0)] public int MinCount;
            public Sprite Icon;
        }
    }
}
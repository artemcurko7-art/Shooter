using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game/Items/Item")]
    public class ItemDefinition : RewardDefinition
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private List<IconVariant> _iconVariants = new();
        [SerializeField] private GameObject _prefab;
        
        public ItemType Type => _type;
        public override GameObject Prefab => _prefab;
        
        public override Sprite GetIcon(int count)
        {
            if (_iconVariants == null || _iconVariants.Count == 0)
                return null;

            Sprite fallback = null;
            Sprite result = null;
            var bestThreshold = int.MinValue;

            foreach (var variant in _iconVariants.Where(v => v != null && v.Icon))
            {
                if (!fallback)
                    fallback = variant.Icon;

                if (count < variant.MinCount || variant.MinCount <= bestThreshold)
                    continue;

                result = variant.Icon;
                bestThreshold = variant.MinCount;
            }

            return result ? result : fallback;
        }

        public override string GetLocalizedName()
        {
            return Localization.GetNameTranslations(_type);
        }

        public override string GetLocalizedTypeName()
        {
            return Localization.GetNameTranslations(_type);
        }

        [Serializable]
        private class IconVariant
        {
            [Min(0)] public int MinCount;
            public Sprite Icon;
        }
    }
}
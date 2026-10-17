using System.Collections.Generic;
using Game.Scripts.UI;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Offer", menuName = "Game/Items/Offer")]
    public class OfferDefinition : RewardDefinition
    {
        [SerializeField] private LocalizedName _nameTranslations;
        [SerializeField, Min(0)] private float _price;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private List<Reward> _rewards = new();

        public override GameObject Prefab => _prefab;
        public IReadOnlyList<Reward> Rewards => _rewards;

        public override Sprite GetIcon(int count)
        {
            if (_icon)
                return _icon;

            return _rewards is { Count: > 0 } ? _rewards[0]?.Icon : null;
        }

        public override string GetLocalizedName()
        {
            return _nameTranslations?.Get(YG2.lang) ?? string.Empty;
        }

        public override string GetLocalizedTypeName()
        {
            return Localization.GetNameTranslations(ItemType.Offer);
        }
    }
}
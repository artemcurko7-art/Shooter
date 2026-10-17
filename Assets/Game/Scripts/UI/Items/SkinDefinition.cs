using UnityEngine;
using YG;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Skin", menuName = "Game/Items/Skin")]
    public class SkinDefinition : RewardDefinition
    {
        [SerializeField] private LocalizedName _nameTranslations;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;

        public override GameObject Prefab => _prefab;

        public override Sprite GetIcon(int count)
        {
            return _icon;
        }

        public override string GetLocalizedName()
        {
            return _nameTranslations?.Get(YG2.lang) ?? string.Empty;
        }

        public override string GetLocalizedTypeName()
        {
            return Localization.GetNameTranslations(ItemType.Skin);
        }
    }
}
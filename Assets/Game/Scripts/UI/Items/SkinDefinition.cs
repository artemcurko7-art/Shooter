using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Skin", menuName = "Game/Items/Skin")]
    public class SkinDefinition : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private LocalizedName _nameTranslations;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;

        public string Id => _id;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;

        public string GetLocalizedName(string languageCode)
        {
            return _nameTranslations?.Get(languageCode) ?? string.Empty;
        }
    }
}
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    public abstract class RewardDefinition : ScriptableObject
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public abstract Sprite GetIcon(int count);
        public abstract string GetLocalizedName();

        public virtual string GetLocalizedTypeName()
        {
            return string.Empty;
        }

        public virtual GameObject Prefab => null;
    }
}
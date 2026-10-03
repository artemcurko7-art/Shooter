using Game.Scripts.Configs;
using Game.Scripts.Equipment.DragInDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public abstract class Slot : MonoBehaviour
    {
        [field: SerializeField] public Image Rarity { get; private set; }
        [field: SerializeField] public Image Icon { get; private set; }

        public DragSlot Drag => GetDrag();
        public RarityEquipmentConfig RarityEquipmentConfig { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public string Name { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(RarityEquipmentConfig rarityEquipmentConfig, Sprite icon, string name)
        {
            RarityEquipmentConfig = rarityEquipmentConfig;
            Rarity.sprite = rarityEquipmentConfig.Icon;
            Icon.sprite = icon;
            Name = name;
        }

        protected abstract DragSlot GetDrag();
    }
}
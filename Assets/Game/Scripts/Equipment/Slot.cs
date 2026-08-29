using Game.Scripts.Configs;
using Game.Scripts.Equipment.DragInDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _rarity;
        [SerializeField] private Image _icon;
        [field: SerializeField] public DragSlot Drag { get; private set; }
        
        public RarityEquipmentConfig RarityEquipmentConfig { get; private set; }
        public EquipmentItem EquipmentItem { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public RectTransform ChildRectTransform { get; private set; }
        
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            ChildRectTransform = _icon.GetComponent<RectTransform>();
        }

        public void Initialize(RarityEquipmentConfig rarityEquipmentConfig, EquipmentItem equipmentItem, Sprite icon)
        {
            RarityEquipmentConfig = rarityEquipmentConfig;
            EquipmentItem = equipmentItem;
            _rarity.sprite = rarityEquipmentConfig.Icon;
            _icon.sprite = icon;
            Drag.Initialize(this);
        }
    }
}
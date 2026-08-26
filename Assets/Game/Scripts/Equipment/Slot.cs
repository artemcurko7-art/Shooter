using Game.Scripts.Configs;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Type;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _rarity;
        [SerializeField] private Image _icon;
        [field: SerializeField] public DragSlot Drag { get; private set; }
        
        public RarityEquipmentType RarityEquipmentType { get; private set; }
        public EquipmentType EquipmentType { get; private set; }
        public EquipmentConfig EquipmentConfig { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public RectTransform ChildRectTransform { get; private set; }
        
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            ChildRectTransform = _icon.GetComponent<RectTransform>();
        }

        public void Initialize(RarityEquipmentType rarityEquipmentType, EquipmentConfig equipmentConfig, Sprite rarity)
        {
            RarityEquipmentType = rarityEquipmentType;
            EquipmentType = equipmentConfig.Type;
            EquipmentConfig = equipmentConfig;
            _rarity.sprite = rarity;
            _icon.sprite = equipmentConfig.Icon;
            Drag.Initialize(this);
        }
    }
}
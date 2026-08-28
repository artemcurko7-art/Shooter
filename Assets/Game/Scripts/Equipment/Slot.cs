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
        public EquipmentItem EquipmentItem { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public RectTransform ChildRectTransform { get; private set; }
        
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            ChildRectTransform = _icon.GetComponent<RectTransform>();
        }

        public void Initialize(RarityEquipmentType rarityEquipmentType, EquipmentItem equipmentItem, Sprite rarity, Sprite icon)
        {
            RarityEquipmentType = rarityEquipmentType;
            EquipmentItem = equipmentItem;
            _rarity.sprite = rarity;
            _icon.sprite = icon;
            Drag.Initialize(this);
        }
    }
}
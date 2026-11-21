using Game.Scripts.Common.Classes.Rarity;
using Game.Scripts.Equipment.EquipmentContext.Repository;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Equipment.EquipmentContext;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext.Replacement
{
    public class EquipmentDisplayReplacement : MonoBehaviour
    {
        [SerializeField] private ChildrenImagesSpriteApplier _childrenImagesSpriteApplierDropped;
        [SerializeField] private ChildrenImagesSpriteApplier _childrenImagesSpriteApplierDragged;
        [SerializeField] private Image _rarityDropped;
        [SerializeField] private Image _rarityDragged;
        [SerializeField] private TMP_Text _nameDropped;
        [SerializeField] private TMP_Text _nameDragged;
        [SerializeField] private Image _iconDropped;
        [SerializeField] private Image _iconDragged;
        
        private IFreeSlotRegistry<EquipmentType, EquipmentSlot> _freeSlotRegistry;
        private IReplacementService _service;
        
        [Inject]
        public void Construct(IFreeSlotRegistry<EquipmentType, EquipmentSlot> freeSlotRegistry, IReplacementService service)
        {
            _freeSlotRegistry = freeSlotRegistry;
            _service = service;
        }
        
        private void OnEnable()
        {
            var typeDropped = _service.EquipmentSlot.EquipmentItem.Type;
            var slotDropped = _freeSlotRegistry.EquippedSlots[typeDropped];
            var rarityConfigDropped = _freeSlotRegistry.EquippedSlots[typeDropped].RarityConfig;
            
            _childrenImagesSpriteApplierDropped.Set(rarityConfigDropped.Background, rarityConfigDropped.Circle);
            _rarityDropped.sprite = slotDropped.RarityConfig.Icon;
            _nameDropped.text = slotDropped.Name;
            _iconDropped.sprite = slotDropped.Icon.sprite;

            var slotDragged = _service.EquipmentSlot;
            var rarityConfigDragged = _service.EquipmentSlot.RarityConfig;
            
            _childrenImagesSpriteApplierDragged.Set(rarityConfigDragged.Background, rarityConfigDragged.Circle);
            _rarityDragged.sprite = slotDragged.RarityConfig.Icon;
            _nameDragged.text = slotDragged.Name;
            _iconDragged.sprite = slotDragged.Icon.sprite;
                
        }
    }
}
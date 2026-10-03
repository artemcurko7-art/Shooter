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
            _rarityDropped.sprite = _freeSlotRegistry.EquippedSlots[_service.EquipmentSlot.EquipmentItem.Type].RarityConfig.Icon;
            _rarityDragged.sprite = _service.EquipmentSlot.RarityConfig.Icon;
            
            _nameDropped.text = _freeSlotRegistry.EquippedSlots[_service.EquipmentSlot.EquipmentItem.Type].Name;
            _nameDragged.text = _service.EquipmentSlot.Name;
                
            _iconDropped.sprite = _freeSlotRegistry.EquippedSlots[_service.EquipmentSlot.EquipmentItem.Type].Icon.sprite;
            _iconDragged.sprite = _service.EquipmentSlot.Icon.sprite;
        }
    }
}
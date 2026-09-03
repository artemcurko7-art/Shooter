using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.Replacement
{
    public class DisplayReplacement : MonoBehaviour
    {
        [SerializeField] private Image _rarityDropped;
        [SerializeField] private Image _rarityDragged;
        [SerializeField] private Image _iconDropped;
        [SerializeField] private Image _iconDragged;
        [SerializeField] private TMP_Text _nameDropped;
        [SerializeField] private TMP_Text _nameDragged;

        private IEquipmentFreeSlotRegistry _freeSlotRegistry;
        private IReplacementService _service;
        
        [Inject]
        public void Construct(IEquipmentFreeSlotRegistry freeSlotRegistry, IReplacementService service)
        {
            _freeSlotRegistry = freeSlotRegistry;
            _service = service;
        }
        
        private void OnEnable()
        {
            _rarityDropped.sprite = _freeSlotRegistry.EquippedSlots[_service.Slot.EquipmentItem.Type].RarityEquipmentConfig.Icon;
            _rarityDragged.sprite = _service.Slot.RarityEquipmentConfig.Icon;
                
            _iconDropped.sprite = _freeSlotRegistry.EquippedSlots[_service.Slot.EquipmentItem.Type].Icon;
            _iconDragged.sprite = _service.Slot.Icon;

            _nameDropped.text = _freeSlotRegistry.EquippedSlots[_service.Slot.EquipmentItem.Type].Name;
            _nameDragged.text = _service.Slot.Name;
        }
    }
}
using Game.Scripts.Service.Equipment;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.Replacement
{
    public class DisplayReplacement : MonoBehaviour
    {
        [SerializeField] private Image _rarity;
        [SerializeField] private Image _icon;
        
        private IEquipmentService _equipmentService;
        
        [Inject]
        public void Construct(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;

            _equipmentService.Added += OnAdded;
        }

        private void OnEnable()
        {
            
        }

        private void OnDestroy()
        {
            _equipmentService.Added -= OnAdded;
        }

        private void OnAdded(Slot slot)
        {
            
        }
    }
}
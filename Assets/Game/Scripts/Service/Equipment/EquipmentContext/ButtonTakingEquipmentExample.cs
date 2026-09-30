using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Service.Equipment.EquipmentContext
{
    public class ButtonTakingEquipmentExample : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private EquipmentSlotRewardService _equipmentService;
        
        [Inject]
        public void Construct(EquipmentSlotRewardService equipmentService)
        {
            _equipmentService = equipmentService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(_equipmentService.Execute);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_equipmentService.Execute);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Service.Equipment
{
    public class ButtonTakingEquipmentExample : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private EquipmentService _equipmentService;
        
        [Inject]
        public void Construct(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(_equipmentService.OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_equipmentService.OnClick);
        }
    }
}
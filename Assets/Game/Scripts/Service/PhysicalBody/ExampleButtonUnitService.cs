using System;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Service.PhysicalBody
{
    public class ExampleButtonUnitService : MonoBehaviour
    {
        [SerializeField] private UnitType _type;
        [SerializeField] private Button _button;

        private UnitService _unitService;
        
        [Inject]
        public void Construct(UnitService unitService)
        {
            _unitService = unitService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _unitService.OnClick(_type);
        }
    }
}
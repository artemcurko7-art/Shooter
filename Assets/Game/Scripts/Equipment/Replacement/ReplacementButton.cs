using Game.Scripts.Service.Equipment;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private ReplacementController _controller;
        private IEquipmentService _equipmentService;
        
        [Inject]
        public void Construct(ReplacementController controller)
        {
            _controller = controller;
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
            _controller.Replace();
        }
    }
}
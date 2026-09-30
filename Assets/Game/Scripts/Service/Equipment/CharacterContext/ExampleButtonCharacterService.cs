using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public class ExampleButtonCharacterService : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private CharacterSlotRewardService _service;
        
        [Inject]
        public void Construct(CharacterSlotRewardService service)
        {
            _service = service;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_service.Execute);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_service.Execute);
        }
    }
}
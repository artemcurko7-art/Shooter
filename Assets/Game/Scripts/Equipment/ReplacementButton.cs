using Game.Scripts.Equipment.CharacterContext.Replacement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment
{
    public abstract class ReplacementButton<T> : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IReplaceable<T> _replaceable;
        
        [Inject]
        public void Construct(IReplaceable<T> replaceable)
        {
            _replaceable = replaceable;
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
            _replaceable.Replace();
        }
    }
}
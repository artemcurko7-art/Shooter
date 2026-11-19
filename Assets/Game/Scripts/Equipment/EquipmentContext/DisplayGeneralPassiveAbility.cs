using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Factory;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class DisplayGeneralPassiveAbility : MonoBehaviour
    {
        private readonly Dictionary<CharacterType, DisplayPassiveAbility> _displays = new();
        
        [SerializeField] private Transform _container;
        
        private DisplayPassiveAbilityFactory _factory;
        private CharacterDropSlot[] _characterDropSlots;
        
        [Inject]
        public void Construct(DisplayPassiveAbilityFactory factory, CharacterDropSlot[] characterDropSlots)
        {
            _factory = factory;
            _characterDropSlots = characterDropSlots;

            //_factory.Create(CharacterType.AttackAircraft, _container); // test
            
            foreach (var characterDropSlot in _characterDropSlots)
            {
                characterDropSlot.Dropped += OnDropped;
                characterDropSlot.Removed += OnRemoved;
            }
        }

        private void OnDestroy()
        {
            foreach (var characterDropSlot in _characterDropSlots)
            {
                characterDropSlot.Dropped -= OnDropped;
                characterDropSlot.Removed -= OnRemoved;
            }
        }

        private void OnDropped(CharacterSlot characterSlot)
        {
            if (_displays.ContainsKey(characterSlot.Type))
                return;
            
            var display = _factory.Create(characterSlot.Type, _container);
            _displays.Add(characterSlot.Type, display);
        }

        private void OnRemoved(CharacterSlot characterSlot)
        {
            bool hasSameType = false;
    
            foreach (var dropSlot in _characterDropSlots)
            {
                if (dropSlot.Slot == null) 
                    continue;

                if (dropSlot.Slot.Type == characterSlot.Type && dropSlot.Slot != characterSlot)
                {
                    hasSameType = true;
                    break;
                }
            }
            
            if (hasSameType == false)
            {
                if (_displays.TryGetValue(characterSlot.Type, out var display))
                {
                    Destroy(display.gameObject);
                    _displays.Remove(characterSlot.Type);
                }
            }
        }
    }
}
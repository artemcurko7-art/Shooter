using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class CharacterSlotFactory
    {
        private readonly CharacterSlot _slot;
        private readonly DiContainer _container;
        
        public CharacterSlotFactory(DiContainer container, CharacterSlot slot)
        {
            _container = container;
            _slot = slot;
        }

        public CharacterSlot Create(RarityEquipmentConfig rarityConfig, CharacterConfig config, Transform container)
        {
            var slot = _container.InstantiatePrefabForComponent<CharacterSlot>(_slot, Vector3.zero, Quaternion.identity, container);
            slot.Initialize(rarityConfig, config.Icon, config.Name);
            slot.Initialize(config.View, config.Stats);
            slot.transform.localScale = Vector3.one;
            
            return slot;
        }
    }
}
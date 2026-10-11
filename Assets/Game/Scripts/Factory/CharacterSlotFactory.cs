using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class CharacterSlotFactory
    {
        private readonly CharacterSlot _slot;
        private readonly StatFactory _statFactory;
        private readonly DiContainer _container;
        
        public CharacterSlotFactory(CharacterSlot slot, StatFactory statFactory, DiContainer container)
        {
            _slot = slot;
            _statFactory = statFactory;
            _container = container;
        }

        public CharacterSlot Create(RarityConfig rarityConfig, CharacterConfig config, Transform container)
        {
            
            var slot = _container.InstantiatePrefabForComponent<CharacterSlot>(_slot, Vector3.zero, Quaternion.identity, container);
            slot.Initialize(rarityConfig, config.Icon, config.Name);

            Dictionary<StatType, Stat> stats = new();

            foreach (var stat in config.Stats)
                stats.Add(stat.Type, _statFactory.Create(stat.Type, stat.Value, stat.IsPercentageValue));
                
            slot.Initialize(config.Type, stats);
            slot.transform.localScale = Vector3.one;
            
            return slot;
        }
    }
}
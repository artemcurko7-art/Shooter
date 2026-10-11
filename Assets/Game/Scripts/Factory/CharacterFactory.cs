using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Factory
{
    public class CharacterFactory
    {
        private readonly StatFactory _statFactory;
        private readonly DiContainer _container;

        public CharacterFactory(StatFactory statFactory, DiContainer container)
        {
            _statFactory = statFactory;
            _container = container;
        }
        
        public Character Create(CharacterConfig config, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<Character>(config.View, container.position, Quaternion.identity, container);

            Dictionary<StatType, Stat> stats = new();
            
            foreach (var stat in config.Stats)
                stats.Add(stat.Type, _statFactory.Create(stat.Type, stat.Value, stat.IsPercentageValue));
            
            view.Initialize(config.Type, config.WeaponType, stats);
            
            return view;
        }
    }
}
using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Factory
{
    public class CharacterFactory
    {
        private readonly List<Stat> _stats = new ();
        private readonly DiContainer _container;

        public CharacterFactory(DiContainer container)
        {
            _container = container;
        }
        
        public Character Create(CharacterConfig config, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<Character>(config.View, container.position, Quaternion.identity, container);
            Fill(config.Stats);
            view.Initialize(config.WeaponType, _stats.ToArray());
            
            return view;
        }

        private void Fill(CharacterStat[] stats)
        {
            _stats.Add(new Health(stats[0].Value, false));
            _stats.Add(new Damage(stats[1].Value, false));
            _stats.Add(new Defence(stats[2].Value, false));
            _stats.Add(new CriticalChance(stats[3].Value, true));
            _stats.Add(new CriticalDamage(stats[4].Value, true));
        }
    }
}
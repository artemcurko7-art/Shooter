using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class UnitFactory 
    {
        private readonly UnitData _data;
        private readonly StatFactory _statFactory;
        private readonly DiContainer _container;
    
        public UnitFactory(UnitData data, StatFactory statFactory, DiContainer container)
        {
            _data = data;
            _statFactory = statFactory;
            _container = container;
        }
    
        public Unit Create(UnitConfig config)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, Vector3.zero, Quaternion.identity, null);

            Dictionary<StatType, Stat> stats = new();

            foreach (var stat in config.Stats)
                stats.Add(stat.Type, _statFactory.Create(stat.Type, stat.Value, false));
            
            unit.Initialize(config.Type, _data.UnitAttackers[config.AttackerType], stats);

            return unit;
        }
    }
}
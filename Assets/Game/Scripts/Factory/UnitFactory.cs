using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.Static.Extensions;
using Game.Scripts.UnitContext;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Registry;
using Game.Scripts.UnitContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class UnitFactory 
    {
        private readonly IUnitData _data;
        private readonly IUnitAttackerCreatorRegistry _creatorRegistry;
        private readonly StatFactory _statFactory;
        private readonly DiContainer _container;
    
        public UnitFactory(
            IUnitData data,
            IUnitAttackerCreatorRegistry creatorRegistry,
            StatFactory statFactory,
            DiContainer container)
        {
            _data = data;                  
            _creatorRegistry = creatorRegistry;
            _statFactory = statFactory;
            _container = container;
        }
    
        public Unit Create(UnitConfig config)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, Vector3.zero, Quaternion.identity, null);

            IReadOnlyDictionary<StatType, Stat> stats = GetCommonStats(config);
            IReadOnlyDictionary<StateType, List<IUnitAttacker>> attackers = GetAttackers(config, unit);
            
            unit.Initialize(config.Type, attackers, stats);

            return unit;
        }

        private IReadOnlyDictionary<StatType, Stat> GetCommonStats(UnitConfig config)
        {
            Dictionary<StatType, Stat> stats = new();

            foreach (var stat in config.Stats)
                stats.Add(stat.Key, _statFactory.Create(stat.Key, stat.Value, false));
            
            return stats;
        }
        
        private IReadOnlyDictionary<StateType, List<IUnitAttacker>> GetAttackers(UnitConfig config, Unit unit)
        {
            Dictionary<StateType, List<IUnitAttacker>> attackers = new();

            foreach (var type in Enum.GetValues(typeof(StateType)))
            {
                var stateType = (StateType)type;
                
                if (stateType == StateType.None)
                    continue;
                
                attackers.Add(stateType, new List<IUnitAttacker>());
            }
            
            foreach (var unitAttackerData in config.AttackerDates)
            {
                foreach (var unitAttackerContext in unitAttackerData.Attackers)
                {
                    Dictionary<StatType, Stat> _stats = new();
                    
                    foreach (var unitStats in unitAttackerContext.Stats)
                        _stats.Add(unitStats.Key, _statFactory.Create(unitStats.Key, unitStats.Value, false));

                    var unitAttacker = CreateAttacker(unitAttackerContext.Type, config, unit, _stats);
                    attackers[unitAttackerData.StateType].Add(unitAttacker);
                }
            }
            
            return attackers;
        }

        private IUnitAttacker CreateAttacker(UnitAttackerType type, UnitConfig config, Unit unit, IReadOnlyDictionary<StatType, Stat> stats)
        {
            switch (type)
            {
                case UnitAttackerType.Thrower:
                    
                    var throwerAttackerCreator = _container.InstantiateThrowerAttackerCreator(_creatorRegistry.Types[type], config, unit);
                    var throwerAttacker = throwerAttackerCreator.Create(stats);

                    return throwerAttacker;
                
                default:
                    
                    var attackerCreator = _container.InstantiateAttackerCreator(_creatorRegistry.Types[type], config);
                    var attacker = attackerCreator.Create(stats);
                    
                    return attacker;
            }
        }
    }
}
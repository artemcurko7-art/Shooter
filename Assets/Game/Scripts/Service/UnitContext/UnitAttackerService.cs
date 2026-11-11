using System.Collections.Generic;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Service.UnitContext;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Creator;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Registry;
using Game.Scripts.UnitContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UnitContext.Repository
{
    public class UnitAttackerService : IUnitAttackerService, ISubscriber
    {
        private readonly IUnitData _data;
        private readonly IUnitService _unitService;
        private readonly IUnitAttackerCreatorRegistry _creatorRegistry;
        private readonly DiContainer _container;
        private readonly Dictionary<Unit, Dictionary<StateType, List<IUnitAttacker>>> _attackers = new();

        public UnitAttackerService(
            IUnitData data,
            IUnitService unitService,
            IUnitAttackerCreatorRegistry creatorRegistry,
            DiContainer container)
        {
            _data = data;
            _unitService = unitService;
            _creatorRegistry = creatorRegistry;
            _container = container;
        }
        
        public void Subscribe()
        {
            _unitService.Added += OnAdded;
        }

        public void Unsubscribe()
        {
            _unitService.Added -= OnAdded;
        }

        public IUnitAttacker Get(Unit unit, StateType stateType)
        {
            return _attackers[unit][stateType][UserUtils.NumberGeneration.GetIntegerRandom(0, _attackers[unit][stateType].Count - 1)];
        }
        
        private void OnAdded(Unit unit)
        {
            if (_attackers.ContainsKey(unit))
                return;

            _attackers.Add(unit, new Dictionary<StateType, List<IUnitAttacker>>());

            foreach (var unitValue in _data.Units.Values)
            {
                foreach (var config in unitValue)
                {
                    foreach (var unitAttackerData in config.AttackerDates)
                    {
                        if (_attackers[unit].ContainsKey(unitAttackerData.StateType) == false)
                        {
                            _attackers[unit].Add(unitAttackerData.StateType, new List<IUnitAttacker>());
                        }

                        foreach (var type in unitAttackerData.Types)
                        {
                            if (type == UnitAttackerType.Thrower)
                                continue;

                            var attacker = (IAttackerCreator)_container.Instantiate(_creatorRegistry.Types[type]);
                            var unitAttacker = attacker.Create(unit.Stats);

                            _attackers[unit][unitAttackerData.StateType].Add(unitAttacker);
                        }
                    }
                }
            }
        }
    }
}
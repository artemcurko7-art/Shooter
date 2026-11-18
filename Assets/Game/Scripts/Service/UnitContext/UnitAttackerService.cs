using System;
using Game.Scripts.HitImpacted;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Service.UnitContext;
using UnityEngine;

namespace Game.Scripts.UnitContext.Repository
{
    public class UnitAttackerService : IUnitAttackerService, ISubscriber
    {
        private readonly IUnitService _unitService;

        public event Action<IImpactReceiver> Added;
        
        public UnitAttackerService(IUnitService unitService)
        {
            _unitService = unitService;
        }
        
        public void Subscribe()
        {
            _unitService.Added += OnAdded;
        }

        public void Unsubscribe()
        {
            _unitService.Added -= OnAdded;
        }
        
        private void OnAdded(Unit unit)
        {
            foreach (var attackers in unit.Attackers.Values)
                foreach (var attacker in attackers)
                    Added?.Invoke(attacker);
        }
    }
}
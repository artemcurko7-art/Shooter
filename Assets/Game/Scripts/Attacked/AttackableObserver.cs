using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.Attacked
{
    public abstract class AttackableObserver : ISubscriber
    {
        private readonly IWeaponShooting[] _shootings;
        
        public AttackableObserver(IWeaponShooting[] shootings)
        {
            _shootings = shootings;
        }
        
        public void Subscribe()
        {
            foreach (var shooting in _shootings)
                shooting.Attacked += OnAttacked;
        }

        public void Unsubscribe()
        {
            foreach (var shooting in _shootings)
                shooting.Attacked -= OnAttacked;
        }

        protected abstract void OnAttacked(IAttacker attacker);
    }
}
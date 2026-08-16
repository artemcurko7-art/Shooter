using System;
using Game.Scripts.Damagable;
using Game.Scripts.FSM;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class Unit : PhysicalBody<Unit>, IDamagable, ITransformable
    {
        private State _state;
        private ITransformable _transformable;
        private int _health;
    
        public event Action<Unit> Disabled;

        public Transform Transform { get; }

        [Inject]
        public void Construct(ITransformable transformable)
        {
            _transformable = transformable;
        }

        private void Update()
        {
            _state.Update();
        }
    
        public void Initialize(IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            _health = health;
        
            _state = new State();
        
            _state.AddState(new UnitStateFollower(_state, transform, _transformable.Transform, speed, distance));
            _state.AddState(new UnitStateAttacker(_state, attacker, transform, _transformable.Transform, distance, damage));
        
            _state.SetState<UnitStateFollower>();
        }

        public void TakeDamage(int damage)
        {
        
        }
    }
}
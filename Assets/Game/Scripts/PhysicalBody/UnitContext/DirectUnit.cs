using System;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class DirectUnit : Unit
    {
        private State _state;

        private void Update()
        {
            _state.Update();
        }

        public override void Initialize(IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            base.Initialize(attacker, health, damage, speed, distance);
            
            _state = new State();
            
            _state.Add(new DirectUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, speed, distance));
            _state.Add(new DirectUnitStateAttacker(_state, Animator, transform, Transformable.Transform, distance));
            
            _state.Set<DirectUnitStateFollower>();
        }

        public override void Attack()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            
            if (Physics.Raycast(ray, out var hit, 5))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    Attacker.Attack(damageable, transform, 5);
                    Debug.Log("Direct Hit");
                }
            }
        }
    }
}
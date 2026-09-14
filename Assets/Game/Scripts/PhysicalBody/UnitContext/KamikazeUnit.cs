using System;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class KamikazeUnit : Unit
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
            
            _state.Add(new KamikazeUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, speed, distance));
            _state.Add(new KamikazeUnitStateAttacker(_state, Animator));
            
            _state.Set<KamikazeUnitStateFollower>();
        }
        
        public override void Attack()
        {
            base.Attack();
            
            Destroy(gameObject);
        }
    }
}
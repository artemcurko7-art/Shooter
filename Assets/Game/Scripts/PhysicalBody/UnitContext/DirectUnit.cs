using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class DirectUnit : Unit
    {
        private State _state;

        private void Update()
        {
            _state.Update();
        }

        public override void Initialize(UnitType type, IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            base.Initialize(type, attacker, health, damage, speed, distance);
            
            _state = new State();
            
            _state.Add(new DirectUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, speed, distance));
            _state.Add(new DirectUnitStateAttacker(_state, Animator, transform, Transformable.Transform, distance));
            
            _state.Set<DirectUnitStateFollower>();
        }
    }
}
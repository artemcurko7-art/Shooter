using System;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PhysicalBody.UnitContext.Type;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class MageUnit : Unit
    {
        private State _state;

        private void Update()
        {
            //_state.Update();
        }
    
        public override void Initialize(UnitType type, IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            base.Initialize(type, attacker, health, damage, speed, distance);

            _state = new State();
            
            _state.Add(new MageUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, 3, 15));
            _state.Add(new MageUnitStateAttacker(_state, Animator, transform, Transformable.Transform, 15));
            
            _state.Set<MageUnitStateFollower>();
        }
    }
}
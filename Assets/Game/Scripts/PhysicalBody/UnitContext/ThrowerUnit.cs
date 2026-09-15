using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class ThrowerUnit : Unit
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
            
            _state.Add(new ThrowerUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, 3, 15));
            _state.Add(new ThrowerUnitStateAttacker(_state, Animator, transform, Transformable.Transform, 15));
            
            _state.Set<ThrowerUnitStateFollower>();
        }
    }
}
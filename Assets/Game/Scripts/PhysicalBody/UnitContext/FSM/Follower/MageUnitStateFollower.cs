using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM.Follower
{
    public class MageUnitStateFollower : UnitStateFollower
    {
        public MageUnitStateFollower(State state, CharacterController characterController, Transform current, Transform target, float speed, float distance)
            : base(state, characterController, current, target, speed, distance) { }
        
        protected override void TransitState()
        {
            State.Set<MageUnitStateAttacker>();
        }
    }
}
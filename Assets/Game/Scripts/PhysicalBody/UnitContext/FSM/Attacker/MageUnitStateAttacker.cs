using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class MageUnitStateAttacker : UnitStateAttacker
    {
        public MageUnitStateAttacker(State state, Animator animator, Transform current, Transform target, float distance)
            : base(state, animator, current, target, distance) { }

        protected override void TransitState()
        {
            State.Set<MageUnitStateFollower>();
        }
    }
}
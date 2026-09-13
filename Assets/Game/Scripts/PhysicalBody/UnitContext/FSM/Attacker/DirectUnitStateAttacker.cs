using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Animation;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker
{
    public class DirectUnitStateAttacker : UnitStateAttacker
    {
        public DirectUnitStateAttacker(State state, Animator animator, Transform current, Transform target, float distance)
            : base(state, animator, current, target, distance) { }
        
        protected override void TransitState()
        {
            State.Set<DirectUnitStateFollower>();
        }
    }
}
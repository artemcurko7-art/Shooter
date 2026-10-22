using Game.Scripts.Animation;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM.Death
{
    public class UnitStateDeath : StateDeath
    {
        private readonly Animator _animator;
        
        public UnitStateDeath(State state, Animator animator) : base(state)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetTrigger(AnimationData.Params.Death);
        }
    }
}
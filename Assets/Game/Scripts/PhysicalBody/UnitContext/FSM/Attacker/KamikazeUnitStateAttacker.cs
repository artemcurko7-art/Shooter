using Game.Scripts.Animation;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class KamikazeUnitStateAttacker : StateMachine
    {
        private readonly Animator _animator;

        public KamikazeUnitStateAttacker(State state, Animator animator) : base(state)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetTrigger(UnitAnimationData.Params.Attack);
        }
    }
}
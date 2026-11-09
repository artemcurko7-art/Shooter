using Game.Scripts.Animation;
using UnityEngine;

namespace Game.Scripts.UnitContext.FSM.Attacker
{
    public class KamikazeUnitStateAttacker : StateAttacker
    {
        private readonly Animator _animator;

        public KamikazeUnitStateAttacker(State state, Animator animator) : base(state)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetTrigger(AnimationData.Params.Attack);
        }
    }
}
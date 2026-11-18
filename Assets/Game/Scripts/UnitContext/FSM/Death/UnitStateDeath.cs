using System.Collections.Generic;
using Game.Scripts.Animation;
using Game.Scripts.Static.UserUtils;
using Game.Scripts.UnitContext.Attacker;
using UnityEngine;

namespace Game.Scripts.UnitContext.FSM.Death
{
    public class UnitStateDeath : StateDeath
    {
        private readonly List<IUnitAttacker> _attackers;
        private readonly Animator _animator;
        private readonly Transform _shootPoint;
        
        public UnitStateDeath(State state, List<IUnitAttacker> attackers, Animator animator, Transform shootPoint) : base(state)
        {
            _attackers = attackers;
            _animator = animator;
            _shootPoint = shootPoint;
        }
        
        public override void Enter()
        {
            if (_attackers.Count > 0)
            {
                var attacker = _attackers[NumberGeneration.GetIntegerRandom(0, _attackers.Count - 1)];
                attacker.Attack(_shootPoint);
            }
            
            _animator.SetTrigger(AnimationData.Params.Death);
        }
    }
}
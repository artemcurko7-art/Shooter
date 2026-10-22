using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Animation;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker
{
    public class UnitStateAttacker : StateAttacker
    {
        private readonly Animator _animator;
        private readonly Transform _current;
        private readonly Transform _target;
        private readonly float _distance;
        private CancellationTokenSource _cancellationTokenSource;
    
        public UnitStateAttacker(State state, Animator animator, Transform current, Transform target, float distance)
            : base(state)
        {
            _animator = animator;
            _current = current;
            _target = target;
            _distance = distance * distance;
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            EnterAsync(_cancellationTokenSource.Token).Forget();
        }

        public override void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid EnterAsync(CancellationToken token)
        {
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                _animator.SetTrigger(AnimationData.Params.Attack);

                await UniTask.Yield(PlayerLoopTiming.Update, token);
                
                var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

                await UniTask.Delay(TimeSpan.FromSeconds(stateInfo.length), cancellationToken: token);
                
                if ((_target.position - _current.position).sqrMagnitude > _distance)
                {
                    State.Set<StateFollower>();
                    _animator.ResetTrigger(AnimationData.Params.Attack);
                    _animator.Play(UnitAnimationData.Params.Run, 0, 0f);

                    return;
                }
                
                await UniTask.Yield();
            }
        }
    }
}
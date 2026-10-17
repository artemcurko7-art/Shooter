using System;
using Game.Scripts.Animation;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private float _dampTime;
        
        private IMover _mover;
        private Animator[] _animators;
        
        [Inject]
        public void Construct(IMover mover)
        {
            _mover = mover;
            _animators = GetComponentsInChildren<Animator>();
        }

        private void Update()
        {
            foreach (var animator in _animators)
                animator.SetFloat(PlayerAnimationData.Params.Speed, _mover.Direction.sqrMagnitude, _dampTime, Time.deltaTime);
        }
    }
}
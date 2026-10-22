using UnityEngine;

namespace Game.Scripts.Animation
{
    public static class PlayerAnimationData
    {
        public static class Params
        {
            public static readonly int Idle = Animator.StringToHash(nameof(Idle));
            public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        }
    }
}
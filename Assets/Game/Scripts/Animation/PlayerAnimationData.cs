using UnityEngine;

namespace Game.Scripts.Animation
{
    public class PlayerAnimationData 
    {
        public static class Params
        {
            public static readonly int Idle = Animator.StringToHash(nameof(Idle));
            public static readonly int Speed = Animator.StringToHash(nameof(Speed));
            public static readonly int Attack = Animator.StringToHash(nameof(Attack));
            public static readonly int Death = Animator.StringToHash(nameof(Death));
        }
    }
}

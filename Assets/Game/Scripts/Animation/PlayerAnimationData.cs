using UnityEngine;

namespace Game.Scripts.Animation
{
    public class PlayerAnimationData 
    {
        public static class Params
        {
            public static readonly int Idle = Animator.StringToHash(nameof(Idle));
            public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
            public static readonly int Attack = Animator.StringToHash(nameof(Attack));
            public static readonly int Death = Animator.StringToHash(nameof(Death));
        }
    }
}

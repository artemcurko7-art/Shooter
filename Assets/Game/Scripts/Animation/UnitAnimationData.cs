using UnityEngine;

namespace Game.Scripts.Animation
{
    public static class UnitAnimationData 
    {
        public static class Params
        {
            public static readonly int Run = Animator.StringToHash(nameof(Run));
            public static readonly int Attack = Animator.StringToHash(nameof(Attack));
            public static readonly int Death = Animator.StringToHash(nameof(Death));
        }
    }
}
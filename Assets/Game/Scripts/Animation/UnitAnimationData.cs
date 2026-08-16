using UnityEngine;

namespace Game.Scripts.Animation
{
    public static class UnitAnimationData 
    {
        public static class Params
        {
            public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
            public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        }
    }
}
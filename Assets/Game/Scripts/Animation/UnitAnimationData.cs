using UnityEngine;

namespace Game.Scripts.Animation
{
    public static class UnitAnimationData
    {
        public static class Params
        {
            public static readonly int Run = Animator.StringToHash(nameof(Run));
        }
    }
}
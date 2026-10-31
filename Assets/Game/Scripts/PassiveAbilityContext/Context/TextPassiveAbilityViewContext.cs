using System;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Context
{
    [Serializable]
    public class TextPassiveAbilityViewContext
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
    }
}
using System;
using Game.Scripts.VFX.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Context
{
    [Serializable]
    public class EffectPassiveAbilityViewContext
    {
        [field: SerializeField] public EffectType Type { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public Vector3 Scale { get; private set; }
    }
}
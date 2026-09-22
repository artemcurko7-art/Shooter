using System;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.Wave
{
    [Serializable]
    public class WaveData
    {
        [field: SerializeField] public UnitType Type { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }
    }
}
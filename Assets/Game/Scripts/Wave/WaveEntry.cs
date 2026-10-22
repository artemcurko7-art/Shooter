using System;
using UnityEngine;

namespace Game.Scripts.Wave
{
    [Serializable]
    public class WaveEntry
    {
        [field: SerializeField] public WaveData[] Waves { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}
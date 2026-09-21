using System;
using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.CharacterContext
{
    [Serializable]
    public class CharacterStat
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
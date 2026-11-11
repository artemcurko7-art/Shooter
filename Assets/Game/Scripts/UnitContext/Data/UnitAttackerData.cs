using System;
using Game.Scripts.UnitContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.UnitContext.Data
{
    [Serializable]
    public class UnitAttackerData
    {
        [field: AllowNesting]
        [field: SerializeField, ReadOnly] public StateType StateType { get; private set; }
        [field: SerializeField, ReadOnly] public UnitAttackerType[] Types { get; private set; }
        
        public UnitAttackerData(StateType stateType)
        {
            StateType = stateType;
        }
    }
}
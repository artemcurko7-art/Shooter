using System;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.UnitContext.Data
{
    [Serializable]
    public class UnitAttackerData
    {
        public const string StateTypeFieldName = nameof(_stateType);
        public const string AttackersContextFieldName = nameof(_attackers);
        
        [SerializeField, ReadOnly] private StateType _stateType;
        [SerializeField] private UnitAttackerContext[] _attackers;
        
        public StateType StateType => _stateType;
        public UnitAttackerContext[] Attackers => _attackers;

        public UnitAttackerData(StateType stateType)
        {
            _stateType = stateType;
            _attackers = Array.Empty<UnitAttackerContext>();
        }
    }
}
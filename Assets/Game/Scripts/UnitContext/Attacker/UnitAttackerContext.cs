using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    [Serializable]
    public class UnitAttackerContext
    {
        public const string TypeFieldName = nameof(_type);
        public const string StatDatesFieldName = nameof(_statDates);
        
        [SerializeField] private UnitAttackerType _type;
        [SerializeField] private UnitStatData[] _statDates;
        
        public UnitAttackerType Type => _type;
        private UnitStatData[] _cachedStatDates;
        private Dictionary<StatType, int> _stats;
        
        public IReadOnlyDictionary<StatType, int> Stats
        {
            get
            {
                if (_statDates == null || _statDates.Length == 0)
                    return _stats ??= new Dictionary<StatType, int>();
                
                if (_stats == null || _cachedStatDates != _statDates)
                {
                    _stats = _statDates
                        .GroupBy(stat => stat.Type)
                        .ToDictionary(type => type.Key, stat => stat.First().Value);
                        
                    _cachedStatDates = _statDates;
                }

                return _stats;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.ProjectileContext.Type;
using Game.Scripts.UnitContext;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Unit", fileName = "Unit", order = 3)]
    public class UnitConfig : ScriptableObject
    {
        public const string AttackerDatesFieldName = nameof(_attackerDates);
        public const string StatDatesFieldName = nameof(_statDates);
        
        [SerializeField] private UnitType _type;
        [SerializeField] private ProjectileType _projectileType;
        [AllowNesting]
        [SerializeField, ReadOnly] private LayerMask _layerMask;
        [SerializeField] private Unit _unit;
        [SerializeField, HideInInspector] private UnitAttackerData[] _attackerDates;
        [SerializeField, HideInInspector] private UnitStatData[] _statDates;
        
        private UnitStatData[] _cachedStatDates;
        private Dictionary<StatType, int> _stats;
        
        public UnitType Type => _type;
        public ProjectileType ProjectileType => _projectileType;
        public LayerMask LayerMask => _layerMask;
        public Unit Unit => _unit;
        public UnitAttackerData[] AttackerDates => _attackerDates;
        
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
        
        // [SerializeField] private UnitAttackerType _type;
        // [SerializeField] private UnitStatData[] _statDates;
        //
        // public UnitAttackerType Type => _type;
        // private UnitStatData[] _cachedStatDates;
        // private Dictionary<StatType, int> _stats;
        //
        // public IReadOnlyDictionary<StatType, int> Stats
        // {
        //     get
        //     {
        //         if (_statDates == null || _statDates.Length == 0)
        //             return _stats ??= new Dictionary<StatType, int>();
        //         
        //         if (_stats == null || _cachedStatDates != _statDates)
        //         {
        //             _stats = _statDates
        //                 .GroupBy(stat => stat.Type)
        //                 .ToDictionary(type => type.Key, stat => stat.First().Value);
        //                 
        //             _cachedStatDates = _statDates;
        //         }
        //
        //         return _stats;
        //     }
        // }
        
        private void OnValidate()
        {
            _layerMask = LayerMask.GetMask(nameof(Unit));
        }
    }
}
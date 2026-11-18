using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Data
{
    public  class UnitRuntimeData
    {
        // // Верхний уровень: StateType -> (UnitAttackerType -> Словарь статов)
        // private readonly Dictionary<StateType, Dictionary<UnitAttackerContext, IReadOnlyDictionary<StatType, int>>> _attackers;
        //
        // // Общие статы (CommonStats), которые не зависят от атаки
        // public IReadOnlyDictionary<StatType, int> CommonStats { get; }
        //
        // public UnitRuntimeData(UnitConfig config)
        // {
        //     CommonStats = config.Stats.ToDictionary(stat => stat.Key, value => value.Value);
        //     
        //     _attackers = new Dictionary<StateType, Dictionary<UnitAttackerContext, IReadOnlyDictionary<StatType, int>>>();
        //
        //     foreach (var attackerData in config.AttackerDates)
        //     {
        //         var stateDict = new Dictionary<UnitAttackerContext, IReadOnlyDictionary<StatType, int>>();
        //
        //         foreach (var context in attackerData.Attackers)
        //         {
        //             stateDict[context] = context.Stats; 
        //         }
        //
        //         _attackers[attackerData.StateType] = stateDict;
        //     }
        // }
        //
        // public IReadOnlyDictionary<StatType, int> GetAttackerStats(StateType stateType, UnitAttackerType attackerType)
        // {
        //     if (_attackers.TryGetValue(stateType, out var stateDict))
        //     {
        //         if (stateDict.TryGetValue(attackerType, out var stats))
        //         {
        //             return stats;
        //         }
        //     }
        //     
        //     return new Dictionary<StatType, int>();
        // }
        //
        // public IReadOnlyDictionary<UnitAttackerType, IReadOnlyDictionary<StatType, int>> GetAttackersForState(StateType stateType)
        // {
        //     return _attackers.TryGetValue(stateType, out var stateDict) 
        //         ? stateDict 
        //         : new Dictionary<UnitAttackerType, IReadOnlyDictionary<StatType, int>>();
        // }
    }
}
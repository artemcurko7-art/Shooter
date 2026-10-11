using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.EquipmentContext.Provider;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Service.Equipment.CharacterContext;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterStatResolver : ICharacterStatResolver
    {
        private const float Percent = 100f;
        private readonly IEquipmentProvider _equipmentProvider;
        private readonly Dictionary<Character, IReadOnlyDictionary<StatType, Stat>> _stats = new();
        private readonly Dictionary<StatType, Stat> _currentStats = new();
        private readonly IReadOnlyDictionary<StatType, Stat> _recordedStats;
        
        public CharacterStatResolver(ICharacterService characterService, IEquipmentProvider equipmentProvider)
        {
            _equipmentProvider = equipmentProvider;
            _recordedStats = characterService.Characters[0].Stats;

            foreach (var stat in _recordedStats)
                _currentStats.Add(stat.Key, stat.Value);
            
            Resolve();

            foreach (var character in characterService.Characters)
                _stats.Add(character, character.Stats);

            // foreach (var characterStat in Stats)
            // {
            //     Debug.Log($"Character type: {characterStat.Key.CharacterType}");
            //     
            //     foreach (var stat in characterStat.Value)
            //     {
            //         Debug.Log($"Stat type: {stat.Key}, Value: {stat.Value.Value}");
            //     }
            // }
        }
        
        public IReadOnlyDictionary<Character, IReadOnlyDictionary<StatType, Stat>> Stats => _stats;

        private void Resolve()
        {
            foreach (var equipmentItem in _equipmentProvider.EquipmentItems)
            {
                if (equipmentItem.Value == null)
                    continue;
                
                Handle(equipmentItem.Value.MainStat);

                foreach (var additionalStat in equipmentItem.Value.AdditionalStats)
                    Handle(additionalStat.Value);
            }
        }

        private void Handle(Stat equipmentStat)
        {
            int bonusValue = CalculateBonusValue(equipmentStat);

            var stats = _recordedStats
                .Where(stat => stat.Key == equipmentStat.Type);

            foreach (var stat in stats)
                stat.Value.Increase(bonusValue);
        }
        
        private int CalculateBonusValue(Stat equipmentStat)
        {
            if (equipmentStat.IsPercentageValue == false)
                return equipmentStat.Value;

            float calculationPercent = equipmentStat.Value / Percent;
            float calculationStat = calculationPercent * _currentStats[equipmentStat.Type].Value;

            return Mathf.CeilToInt(calculationStat);
        }
    }
}
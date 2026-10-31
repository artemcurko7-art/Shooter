using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.EquipmentContext.Provider;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterStatResolver : ICharacterStatResolver, ISubscriber
    {
        private const float Percent = 100f;
        private readonly ICharacterService _characterService;
        private readonly IEquipmentProvider _equipmentProvider;
        private readonly Health _health;
        private readonly Dictionary<StatType, Stat> _currentStats = new();
        private readonly IReadOnlyDictionary<StatType, Stat> _recordedStats;
        private readonly List<Character> _characters = new();
        
        public CharacterStatResolver(ICharacterService characterService, IEquipmentProvider equipmentProvider, Health health)
        {
            _characterService = characterService;
            _equipmentProvider = equipmentProvider;
            _health = health;
            _recordedStats = characterService.Characters[0].Stats;

            foreach (var stat in _recordedStats)
                _currentStats.Add(stat.Key, stat.Value);
            
            Resolve();
            
            foreach (var character in characterService.Characters)
            {
                _characters.Add(character);
                _health.Increase(character.Stats[StatType.Health].Value);
            }
        }
        
        public IReadOnlyList<Character> Characters => _characters;
        
        public void Subscribe()
        {
            foreach (var character in _characterService.Characters)
                character.Stats[StatType.Health].ValueChanged += OnValueChanged;
        }

        public void Unsubscribe()
        {
            foreach (var character in _characterService.Characters)
                character.Stats[StatType.Health].ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(int value)
        {
            _health.Increase(value - _health.Value);
        }
        
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
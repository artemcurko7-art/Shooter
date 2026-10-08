using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.EquipmentContext.Provider;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Service.Equipment.CharacterContext;
using UnityEngine;

namespace Game.Scripts.MV.StatContext.Repository
{
    public class StatRepository
    {
        private const float Percent = 100f;
        private readonly ICharacterService _characterService;
        private readonly IEquipmentProvider _equipmentProvider;
        private readonly Dictionary<StatType, Stat> _stats = new();
        private readonly Dictionary<StatType, int> _defaultStats = new();
        
        public StatRepository(ICharacterService characterService, IEquipmentProvider equipmentProvider)
        {
            _characterService = characterService;
            _equipmentProvider = equipmentProvider;

            foreach (var stat in _characterService.Characters[0].Stats)
                _defaultStats.Add(stat.Type, stat.Value);
            
            Fill();

            _stats.Add(StatType.Health, new Health(0, false));
            _stats.Add(StatType.Damage, new Damage(0, false));
            _stats.Add(StatType.Defence, new Defence(0, false));
            _stats.Add(StatType.CriticalChance, new CriticalChance(0, true));
            _stats.Add(StatType.CriticalDamage, new CriticalDamage(0, true));

            foreach (var character in characterService.Characters)
                foreach (var stat in character.Stats)
                    _stats[stat.Type].Increase(stat.Value);
        }

        public IReadOnlyDictionary<StatType, Stat> Stats => _stats;

        private void Fill()
        {
            foreach (var equipmentItem in _equipmentProvider.EquipmentItems)
            {
                if (equipmentItem.Value == null)
                    continue;
                
                Handle(equipmentItem.Value.MainStat);

                foreach (var additionalStat in equipmentItem.Value.AdditionalStats)
                    Handle(additionalStat);
            }
            
            // Debug.Log($"Health stat: {_characterService.Characters[0].Stats[0].Value}");
            // Debug.Log($"Damage stat: {_characterService.Characters[0].Stats[1].Value}");
            // Debug.Log($"Defence stat: {_characterService.Characters[0].Stats[2].Value}");
            // Debug.Log($"CriticalChance stat: {_characterService.Characters[0].Stats[3].Value}");
            // Debug.Log($"CriticalDamage stat: {_characterService.Characters[0].Stats[4].Value}");
        }

        private void Handle(Stat equipmentStat)
        {
            int bonusValue = CalculateBonusValue(equipmentStat);

            var stats = _characterService.Characters[0].Stats
                .Where(stat => stat.Type == equipmentStat.Type);

            foreach (var stat in stats)
                stat.Increase(bonusValue);
        }
        
        private int CalculateBonusValue(Stat equipmentStat)
        {
            if (equipmentStat.IsPercentageValue == false)
                return equipmentStat.Value;

            float calculationPercent = equipmentStat.Value / Percent;
            float calculationStat = calculationPercent * _defaultStats[equipmentStat.Type];

            return Mathf.CeilToInt(calculationStat);
        }
    }
}
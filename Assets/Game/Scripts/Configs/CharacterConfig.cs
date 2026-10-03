using System;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Character", fileName = "Character", order = 2)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public CharacterType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Character View { get; private set; }
        [field: SerializeField] public CharacterStat[] Stats { get; private set; }

        private void Reset()
        {
            RebuildStatsArray();
        }

        private void OnValidate()
        {
            // Получаем типы без 0-го элемента для проверки длины
            var requiredTypes = GetFilteredStatTypes();

            if (Stats == null || Stats.Length != requiredTypes.Length)
            {
                RebuildStatsArray();
            }
        }

        private void RebuildStatsArray()
        {
            var statTypes = GetFilteredStatTypes();
            var newStats = new CharacterStat[statTypes.Length];

            for (int i = 0; i < statTypes.Length; i++)
            {
                var currentType = statTypes[i];
                int existingValue = 0;
                
                if (Stats != null)
                {
                    var existingStat = Stats.FirstOrDefault(characterStat => characterStat != null && characterStat.Type == currentType);
                    existingValue = existingStat.Value;
                }

                newStats[i] = new CharacterStat(currentType, existingValue);
            }

            Stats = newStats;
        }

        private StatType[] GetFilteredStatTypes()
        {
            return Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .Where(statType => Convert.ToInt32(statType) != 0)
                .ToArray();
        }
    }
}
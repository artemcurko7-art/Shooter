using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.Genetic
{
    [CreateAssetMenu(fileName = "AdditionsData", menuName = "Data/Additions Data")]
    public class AdditionData : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            [SerializeField] private RewardDefinition _definition;
            [SerializeField, Min(0f)] private float _chance = 1f;
            [SerializeField, Min(1)] private int _count = 1;

            public RewardDefinition Definition => _definition;
            public float Chance => _chance;
            public int Count => _count;
        }

        [SerializeField] private List<Entry> _entries = new();

        public IReadOnlyList<Entry> Entries => _entries;

        public bool TryGetRandomReward(out RewardDefinition definition, out int count)
        {
            definition = null;
            count = 1;

            if (_entries == null || _entries.Count == 0)
                return false;

            var totalChance = 0f;

            foreach (var entry in _entries)
            {
                if (entry == null || !entry.Definition || entry.Chance <= 0f)
                    continue;

                totalChance += entry.Chance;
            }

            if (totalChance <= 0f)
                return false;

            var randomValue = UnityEngine.Random.value * totalChance;

            foreach (var entry in _entries)
            {
                if (entry == null || !entry.Definition || entry.Chance <= 0f)
                    continue;

                randomValue -= entry.Chance;

                if (randomValue > 0f)
                    continue;

                definition = entry.Definition;
                count = Mathf.Max(1, entry.Count);
                return true;
            }

            return false;
        }
    }
}
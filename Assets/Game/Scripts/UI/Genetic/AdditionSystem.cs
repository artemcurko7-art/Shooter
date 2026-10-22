
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.UI.Items;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class AdditionSystem : MonoBehaviour
    {
        [Serializable]
        private class AdditionCategory
        {
            [SerializeField] private string _name = "Addition";
            [SerializeField, Min(0)] private int _initialCount = 5;
            [SerializeField, Min(0)] private int _upgradeInterval = 3;
            [SerializeField] private AdditionData _data;
            [SerializeField] private AdditionBar _prefab;
            [SerializeField] private RectTransform _container;

            private readonly List<AdditionBar> _bars = new();
            private int _lastTriggeredUpgradeCount;

            public string Name => _name;
            public int InitialCount => _initialCount;
            public int UpgradeInterval => _upgradeInterval;
            public AdditionData Data => _data;
            public AdditionBar Prefab => _prefab;
            public RectTransform Container => _container;
            public List<AdditionBar> Bars => _bars;

            public void InitializeUpgradeCount(int currentCount)
            {
                _lastTriggeredUpgradeCount = UpgradeInterval > 0
                    ? currentCount - currentCount % UpgradeInterval
                    : currentCount;
            }

            public bool ShouldSpawn(int currentCount)
            {
                if (UpgradeInterval <= 0 || currentCount <= 0)
                    return false;

                if (currentCount % UpgradeInterval != 0)
                    return false;

                if (_lastTriggeredUpgradeCount == currentCount)
                    return false;

                _lastTriggeredUpgradeCount = currentCount;
                return true;
            }
        }

        [Header("Зависимости")]
        [SerializeField] private GeneticElementSpawner _spawner;
        [SerializeField] private GeneticPreview _geneticPreview;

        [Header("Независимые системы дополнений")]
        [SerializeField] private List<AdditionCategory> _categories = new();

        private bool _isInitialized;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            if (!_spawner)
            {
                Debug.LogError(
                    "[AdditionSystem] Не назначен GeneticElementSpawner!",
                    this);
                return;
            }

            var currentCount = YG2.saves.IdSavedStatCount;

            foreach (var category in _categories)
            {
                if (!ValidateCategory(category))
                    continue;

                category.InitializeUpgradeCount(currentCount);

                for (var i = 0; i < category.InitialCount; i++)
                {
                    if (!TryCreateAddition(category))
                        break;
                }

                RebuildLayout(category);
            }

            _isInitialized = true;
        }

        public void OpenPreview(RewardDefinition definition, int count, Vector3 startPosition)
        {
            if (!definition || !_geneticPreview)
            {
                Debug.LogWarning(
                    "[AdditionSystem] Не удалось открыть превью дополнения.",
                    this);
                return;
            }

            _geneticPreview.gameObject.SetActive(true);
            _geneticPreview.Open(definition, count, startPosition);
        }

        public void OnStatUpgraded()
        {
            if (!_isInitialized)
                return;

            var currentCount = YG2.saves.IdSavedStatCount;

            foreach (var category in from category in _categories 
                     where ValidateCategory(category) 
                     where category.ShouldSpawn(currentCount) 
                     where TryCreateAddition(category) select category)
            {
                RebuildLayout(category);
            }
        }

        private bool TryCreateAddition(AdditionCategory category)
        {
            if (!category.Data.TryGetRandomReward(out var definition, out var count))
            {
                Debug.LogWarning(
                    $"[AdditionSystem] Не удалось получить награду: {category.Name}.",
                    this);
                return false;
            }

            var bar = _spawner.Spawn(category.Prefab, category.Container);

            if (!bar)
                return false;

            bar.Init(this, definition, count);
            category.Bars.Add(bar);

            return true;
        }

        private bool ValidateCategory(AdditionCategory category)
        {
            if (category != null && category.Data && category.Prefab && category.Container)
                return true;

            Debug.LogWarning(
                "[AdditionSystem] У категории не назначены необходимые зависимости.",
                this);
            return false;
        }

        private void RebuildLayout(AdditionCategory category)
        {
            if (!category.Container)
                return;

            LayoutRebuilder.ForceRebuildLayoutImmediate(category.Container);
            Canvas.ForceUpdateCanvases();
        }
    }
}

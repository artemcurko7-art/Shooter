
using Game.Scripts.UI.Items;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        [Header("Компоненты")]
        [SerializeField] private StatUpgradeSystem _statUpgradeSystem;
        [SerializeField] private AdditionSystem _additionSystem;
        [SerializeField] private GeneticScrollController _scrollController;
        [SerializeField] private GeneticPreview _geneticPreview;

        public float IncreaseNumber =>
            _statUpgradeSystem ? _statUpgradeSystem.IncreaseNumber : 0f;

        private bool _isInitialized;

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (!_isInitialized)
                return;

            RefreshUI();
            _scrollController.RefreshAndScroll(false);
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            if (!_statUpgradeSystem || !_additionSystem || !_scrollController)
            {
                Debug.LogError(
                    "[GeneticSystem] Не назначены StatUpgradeSystem, AdditionSystem или " +
                    "GeneticScrollController!",
                    this);
                return;
            }

            _additionSystem.Initialize();
            _scrollController.Initialize();

            _isInitialized = true;
        }

        public static bool IsNextStat(int statId)
        {
            return statId > YG2.saves.IdSavedStatCount;
        }

        public static bool IsAvailableStat(int statId)
        {
            return statId == YG2.saves.IdSavedStatCount;
        }

        public static bool IsAlreadyUnlocked(int statId)
        {
            return statId < YG2.saves.IdSavedStatCount;
        }

        public static float GetStatValue(string statName)
        {
            return StatUpgradeSystem.GetStatValue(statName);
        }

        public static float GetStatValue(StatsData.Stat stat)
        {
            return stat == null ? 0f : GetStatValue(stat.name);
        }

        public void IncreaseStat(string statName)
        {
            if (string.IsNullOrEmpty(statName))
                return;

            var stat = new StatsData.Stat
            {
                name = statName
            };

            IncreaseStat(stat, YG2.saves.IdSavedStatCount);
        }

        public void IncreaseStat(StatsData.Stat stat, int index)
        {
            if (!_statUpgradeSystem || stat == null)
                return;

            if (!IsAvailableStat(index))
            {
                Debug.LogWarning(
                    $"[GeneticSystem] Характеристика с индексом {index} недоступна.",
                    this);
                return;
            }

            if (!_statUpgradeSystem.TryIncrease(stat, index))
                return;

            Debug.Log(
                $"[GeneticSystem] Улучшена характеристика {stat.name}. " +
                $"Новый индекс: {YG2.saves.IdSavedStatCount}",
                this);

            _additionSystem?.OnStatUpgraded();

            if (!_scrollController)
                return;

            _scrollController.EnsureVisibleRange();
            _scrollController.RefreshAndScroll(true);
        }

        public void OpenPreview(StatsData.Stat stat, int index, Vector3 statPosition)
        {
            if (stat == null || !_geneticPreview)
            {
                Debug.LogWarning(
                    "[GeneticSystem] Не удалось открыть превью характеристики.",
                    this);
                return;
            }

            _geneticPreview.gameObject.SetActive(true);
            _geneticPreview.Open(stat, index, statPosition);
        }

        public void OpenPreview(RewardDefinition addition, int count, Vector3 additionPosition)
        {
            if (!addition || !_geneticPreview)
            {
                Debug.LogWarning(
                    "[GeneticSystem] Не удалось открыть превью дополнения.",
                    this);
                return;
            }

            _geneticPreview.gameObject.SetActive(true);
            _geneticPreview.Open(addition, count, additionPosition);
        }

        private void RefreshUI()
        {
            if (!_scrollController)
                return;

            _scrollController.RefreshUI();
        }
    }
}

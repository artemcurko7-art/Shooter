using Game.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        private readonly List<StatBar> _statBars = new List<StatBar>();

        [SerializeField] private float _statIncreaseNumber = 0.5f;
        [SerializeField] private int _additionallyStatVisibleCount = 5;

        [SerializeField] private float _uvSpeed = 2000f;

        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Preview _preview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _gridContainer;
        [SerializeField] private RawImage _background;

        public float IncreaseNumber => _statIncreaseNumber;

        private void OnEnable()
        {
            ScrollToNextAvailable();
        }

        private void Start()
        {
            InitializeStats();
            ScrollToNextAvailable();
        }

        private void LateUpdate()
        {
            if (_background == null || _gridContainer == null) return;

            Rect rect = _background.uvRect;
            rect.y = _gridContainer.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;
        }

        private void InitializeStats()
        {
            if (_statsData == null || _statsData.Stats.Count == 0)
            {
                Debug.LogError("[GeneticSystem] StatsData не назначен или список статов пуст!");
                return;
            }

            _statBars.Clear();

            int unlockedCount = YG2.saves.IdSavedStatCount;
            int totalBars = unlockedCount + _additionallyStatVisibleCount;

            for (int i = 0; i < totalBars; i++)
            {
                int statIndexInList = i % _statsData.Stats.Count;
                var stat = _statsData.Stats[statIndexInList];

                StatBar statBar = Instantiate(_statBarPrefab, _gridContainer);

                statBar.Init(this, stat, i);

                _statBars.Add(statBar);
            }

            ScrollToNextAvailable();
            RefreshUI();
        }

        public void EnsureVisibleRange()
        {
            if (_statsData == null || _statsData.Stats.Count == 0) return;

            int unlockedCount = YG2.saves.IdSavedStatCount;
            int totalNeeded = unlockedCount + _additionallyStatVisibleCount;

            while (_statBars.Count < totalNeeded)
            {
                int i = _statBars.Count;
                int statIndexInList = i % _statsData.Stats.Count;
                var stat = _statsData.Stats[statIndexInList];

                StatBar statBar = Instantiate(_statBarPrefab, _gridContainer);
                statBar.Init(this, stat, i);

                _statBars.Add(statBar);
                Debug.Log($"[GeneticSystem] Добавлена новая ячейка стата #{i} ({stat.name})");
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (var bar in _statBars)
            {
                bar.UpdateDisplay();
            }
        }

        public float GetStatValue(string statName)
        {
            return statName switch
            {
                "AttackStrength" => YG2.saves.AttackStrength,
                "CriticalDamage" => YG2.saves.CriticalDamage,
                "Armor" => YG2.saves.Armor,
                "MovementSpeed" => YG2.saves.MovementSpeed,
                "ViewRange" => YG2.saves.ViewRange,
                _ => 0
            };
        }

        public bool IsNextAvailableStat(int statId)
        {
            int nextIndex = YG2.saves.IdSavedStatCount;
            return statId == nextIndex;
        }

        public bool IsAlreadyUnlocked(int statId)
        {
            int nextIndex = YG2.saves.IdSavedStatCount;
            return statId < nextIndex;
        }

        public void IncreaseStat(string statName)
        {
            switch (statName)
            {
                case "AttackStrength": YG2.saves.AttackStrength += _statIncreaseNumber; break;
                case "CriticalDamage": YG2.saves.CriticalDamage += _statIncreaseNumber; break;
                case "Armor": YG2.saves.Armor += _statIncreaseNumber; break;
                case "MovementSpeed": YG2.saves.MovementSpeed += _statIncreaseNumber; break;
                case "ViewRange": YG2.saves.ViewRange += _statIncreaseNumber; break;
                default: return;
            }

            YG2.SaveProgress();

            EnsureVisibleRange();
            ScrollToNextAvailable();
        }

        public void OpenPreview(StatsData.Stat stat, RectTransform statPosition)
        {
            _preview.gameObject.SetActive(true);
            _preview.Open(stat, statPosition);
        }

        public void ScrollToNextAvailable()
        {
            if (_gridContainer == null || _scrollRect == null)
            {
                Debug.LogError("Компоненты не инициализированы!");
                return;
            }

            int nextStatIndex = YG2.saves.IdSavedStatCount;

            if (nextStatIndex >= _statBars.Count)
            {
                Debug.LogWarning("Нет доступных статов для прокрутки!");
                return;
            }

            RectTransform statPosition = _statBars[nextStatIndex].GetComponent<RectTransform>();

            float targetHeight = statPosition.sizeDelta.y;

            Vector2 targetPosition = statPosition.anchoredPosition;

            Vector2 contentSize = _scrollRect.content.sizeDelta;
            Vector2 containerSize = _gridContainer.sizeDelta;

            Vector2 contentPosition = statPosition.InverseTransformPoint(_scrollRect.content.position);
            float targetY = contentPosition.y + targetPosition.y;

            float contentHeight = contentSize.y;
            float containerHeight = containerSize.y;

            float minScroll = Mathf.Max(0, (containerHeight - contentHeight) / 2);
            float maxScroll = Mathf.Min(0, (containerHeight - contentHeight) / 2 + contentHeight);

            float centerOffset = containerHeight / 2 - targetHeight / 2;
            float clampedY = Mathf.Clamp(targetY - centerOffset, minScroll, maxScroll);

            float normalizedY = Mathf.Clamp01((clampedY - minScroll) / (maxScroll - minScroll));

            _scrollRect.verticalNormalizedPosition = 1 - normalizedY;

            _gridContainer.anchoredPosition = new Vector2(
                _gridContainer.anchoredPosition.x,
                -contentHeight / 2 + clampedY
            );
        }
    }
}
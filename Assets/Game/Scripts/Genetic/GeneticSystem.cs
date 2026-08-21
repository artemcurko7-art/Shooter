using Game.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
        [SerializeField] private ScrollView _scrollRect;
        [SerializeField] private Preview _preview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _gridContainer;
        [SerializeField] private RawImage _background;

        public float IncreaseNumber => _statIncreaseNumber;

        private void Start()
        {
            InitializeStats();
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
        }

        public void OpenPreview(StatsData.Stat stat, Vector3 startPosition)
        {
            _preview.gameObject.SetActive(true);
            _preview.Open(stat, startPosition);
        }

        public void ScrollToNextAvailable()
        {
            if (_gridContainer == null || _scrollRect == null) return;

            int nextIndex = YG2.saves.IdSavedStatCount;

            EnsureVisibleRange();

            //StatBar targetBar = _statBars.Find(b => b._ == nextIndex);

            //if (targetBar == null)
            //{
            //    Debug.LogWarning("[GeneticSystem] Не найден бар для следующего стата!");
            //    return;
            //}

            //_scrollRect.ScrollTo(targetBar);
        }
    }
}

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

        [Header("Зависимости")]
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
            Rect rect = _background.uvRect;
            rect.y = _gridContainer.anchoredPosition.y / 2000f;
            _background.uvRect = rect;
        }



        private void InitializeStats()
        {
            if (_statsData == null)
            {
                Debug.LogError("[GeneticSystem] StatsData не назначен в инспекторе!");
                return;
            }

            _statBars.Clear();

            for (int i = 0; i < _statsData.Stats.Count; i++)
            {
                StatBar statBar = Instantiate(_statBarPrefab, _gridContainer);
                statBar.Init(this, _statsData.Stats[i], i);
                _statBars.Add(statBar);
            }

            foreach (var bar in _statBars)
                bar.UpdateDisplay();
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

        public bool IsNextAvailableStat(int statIndex)
        {
            int nextIndex = YG2.saves.IdSavedStatCount;
            return statIndex == nextIndex && statIndex < _statsData.Stats.Count;
        }

        public bool IsAlreadyUnlocked(int statIndex)
        {
            int nextIndex = YG2.saves.IdSavedStatCount;
            return statIndex < nextIndex;
        }

        public void IncreaseStat(string statName)
        {
            switch (statName)
            {
                case "AttackStrength":
                    YG2.saves.AttackStrength += _statIncreaseNumber;
                    break;
                case "CriticalDamage":
                    YG2.saves.CriticalDamage += _statIncreaseNumber;
                    break;
                case "Armor":
                    YG2.saves.Armor += _statIncreaseNumber;
                    break;
                case "MovementSpeed":
                    YG2.saves.MovementSpeed += _statIncreaseNumber;
                    break;
                case "ViewRange":
                    YG2.saves.ViewRange += _statIncreaseNumber;
                    break;
                default:
                    return;
            }

            YG2.SaveProgress();

            foreach (var bar in _statBars)
                bar.UpdateDisplay();
        }

        public void OpenPreview(StatsData.Stat stat, Vector3 startPosition)
        {
            _preview.gameObject.SetActive(true);
            _preview.Open(stat, startPosition);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game.Scripts.Character.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private Transform _gridContainer;

        private List<StatBar> _statBars = new List<StatBar>();

        private void Start()
        {
            foreach (var stat in _statsData.Stats)
            {
                StatBar statBar = Instantiate(_statBarPrefab, _gridContainer);
                statBar.Init(this, stat);
                statBar.RefrashDisplay();
                _statBars.Add(statBar);
            }
        }

        public int GetStatCount(string statName)
        {
            switch (statName)
            {
                case "AttackStrength":
                    return YG2.saves.AttackStrength;
                case "CriticalDamage":
                    return YG2.saves.CriticalDamage;
                case "Armor":
                    return YG2.saves.Armor;
                case "MovementSpeed":
                    return YG2.saves.MovementSpeed;
                case "ViewRange":
                    return YG2.saves.ViewRange;
                default:
                    break;
            }

            return 0;
        }

        public void IncreaseStatCount(string statName)
        {
            switch (statName)
            {
                case "AttackStrength":
                    YG2.saves.AttackStrength++;
                    break;
                case "CriticalDamage":
                    YG2.saves.CriticalDamage++;
                    break;
                case "Armor":
                    YG2.saves.Armor++;
                    break;
                case "MovementSpeed":
                    YG2.saves.MovementSpeed++;
                    break;
                case "ViewRange":
                    YG2.saves.ViewRange++;
                    break;
            }

            YG2.SaveProgress();
        }
    }
}

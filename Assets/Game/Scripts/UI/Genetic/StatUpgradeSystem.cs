using UnityEngine;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class StatUpgradeSystem : MonoBehaviour
    {
        [SerializeField] private float _increaseNumber = 0.5f;

        public float IncreaseNumber => _increaseNumber;

        public static float GetStatValue(string statName)
        {
            return statName switch
            {
                "AttackStrength" => YG2.saves.AttackStrength,
                "CriticalDamage" => YG2.saves.CriticalDamage,
                "Armor" => YG2.saves.Armor,
                "MovementSpeed" => YG2.saves.MovementSpeed,
                "ViewRange" => YG2.saves.ViewRange,
                _ => 0f
            };
        }

        public bool TryIncrease(StatsData.Stat stat, int index)
        {
            if (stat == null || !GeneticSystem.IsAvailableStat(index))
                return false;

            switch (stat.name)
            {
                case "AttackStrength":
                    YG2.saves.AttackStrength += _increaseNumber;
                    break;

                case "CriticalDamage":
                    YG2.saves.CriticalDamage += _increaseNumber;
                    break;

                case "Armor":
                    YG2.saves.Armor += _increaseNumber;
                    break;

                case "MovementSpeed":
                    YG2.saves.MovementSpeed += _increaseNumber;
                    break;

                case "ViewRange":
                    YG2.saves.ViewRange += _increaseNumber;
                    break;

                default:
                    Debug.LogWarning($"[StatUpgradeSystem] Неизвестная характеристика: {stat.name}", this);
                    return false;
            }

            YG2.saves.IdSavedStatCount++;
            YG2.SaveProgress();
            return true;
        }
    }
}
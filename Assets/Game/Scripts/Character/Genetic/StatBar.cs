using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static Game.Scripts.Character.Genetic.StatsData;
using YG;

namespace Game.Scripts.Character.Genetic
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statCount;
        [SerializeField] private Slider _slider;

        private GeneticSystem _geneticSystem;
        private Stat _stat;
        private int _maxValue;

        public void Init(GeneticSystem geneticSystem, Stat stat)
        {
            _geneticSystem = geneticSystem;
            _stat = stat;
            _statName.text = stat.GetLocalizedName(YG2.lang);

            _maxValue = stat.maxCount;

            _slider.maxValue = _maxValue;
        }

        public void IncreaseStat()
        {
            if (_geneticSystem.GetStatCount(_stat.name) >= _maxValue)
                return;

            _geneticSystem.IncreaseStatCount(_stat.name);
            RefrashDisplay();
        }

        public void RefrashDisplay()
        {
            int count = _geneticSystem.GetStatCount(_stat.name);
            _statCount.text = $"{count} / {_maxValue}";
            _slider.value = count;
        }
    }
}
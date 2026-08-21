using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Genetic
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;

        private GeneticSystem _geneticSystem;
        private StatsData.Stat _stat;

        public int Id { get; private set; }

        public void Init(GeneticSystem geneticSystem, StatsData.Stat stat, int id)
        {
            _geneticSystem = geneticSystem;
            _stat = stat;
            Id = id;

            _icon.sprite = _stat.icon;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            bool isNextAvailable = _geneticSystem.IsNextAvailableStat(Id);
            bool isAlreadyUnlocked = _geneticSystem.IsAlreadyUnlocked(Id);

            if (isNextAvailable)
            {
                _button.enabled = true;
                _button.interactable = true;
                _frame.color = Color.green;

                if (_lockOverlay != null) _lockOverlay.SetActive(false);
                if (_checkMark != null) _checkMark.SetActive(false);
            }
            else if (isAlreadyUnlocked)
            {
                _button.enabled = false;
                _button.interactable = false;
                _frame.color = Color.white;

                if (_lockOverlay != null) _lockOverlay.SetActive(false);
                if (_checkMark != null) _checkMark.SetActive(true);
            }
            else
            {
                _button.enabled = false;
                _button.interactable = false;

                if (_lockOverlay != null) _lockOverlay.SetActive(true);
                if (_checkMark != null) _checkMark.SetActive(false);
            }
        }


        public void OnClick()
        {
            _geneticSystem.OpenPreview(_stat, transform.position);
        }
    }
}

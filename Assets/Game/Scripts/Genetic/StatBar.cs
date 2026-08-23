using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Genetic
{
    [RequireComponent(typeof(RectTransform))]
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;

        private GeneticSystem _geneticSystem;
        private StatsData.Stat _stat;
        private RectTransform _rectTransform;
        private int _index;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(GeneticSystem geneticSystem, StatsData.Stat stat, int index)
        {
            _geneticSystem = geneticSystem;
            _stat = stat;
            _index = index;

            _icon.sprite = stat.icon;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            bool isNextAvailable = _geneticSystem.IsNextAvailableStat(_index);
            bool isAlreadyUnlocked = _geneticSystem.IsAlreadyUnlocked(_index);

            if (isNextAvailable)
            {
                _button.enabled = true;
                _button.interactable = true;
                _frame.color = Color.green;

                if (_lockOverlay != null)
                    _lockOverlay.SetActive(false);

                if (_checkMark != null)
                    _checkMark.SetActive(false);
            }
            else if (isAlreadyUnlocked)
            {
                _button.interactable = true;
                _button.enabled = false;
                _frame.color = Color.white;

                if (_lockOverlay != null)
                    _lockOverlay.SetActive(false);

                if (_checkMark != null)
                    _checkMark.SetActive(true);
            }
            else
            {
                _button.interactable = true;
                _button.enabled = false;

                if (_lockOverlay != null)
                    _lockOverlay.SetActive(true);

                if (_checkMark != null)
                    _checkMark.SetActive(false);
            }
        }

        public void OnClick()
        {
            _geneticSystem.OpenPreview(_stat, _rectTransform);
        }
    }
}

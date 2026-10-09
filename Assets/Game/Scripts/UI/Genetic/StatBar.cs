using Game.Scripts.Genetic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Genetic
{
    [RequireComponent(typeof(RectTransform))]
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _darkFrame;

        private GeneticSystem _geneticSystem;
        private StatsData.Stat _stat;
        private RectTransform _rectTransform;

        public int Index { get; private set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(GeneticSystem geneticSystem, StatsData.Stat stat, int index)
        {
            _geneticSystem = geneticSystem;
            Bind(stat, index);
        }

        private void Bind(StatsData.Stat stat, int index)
        {
            _stat = stat;
            Index = index;

            if (_icon)
                _icon.sprite = stat.icon;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            var isAvailable = GeneticSystem.IsAvailableStat(Index);
            var isAlreadyUnlocked = GeneticSystem.IsAlreadyUnlocked(Index);
            var isNext = GeneticSystem.IsNextStat(Index);

            if (isAvailable)
            {
                if (_button) _button.enabled = true;
                if (_frame) _frame.color = Color.green;
                if (_darkFrame) _darkFrame.enabled = false;
                if (_lockOverlay) _lockOverlay.SetActive(false);
                if (_checkMark) _checkMark.SetActive(false);
            }
            else if (isAlreadyUnlocked)
            {
                if (_button) _button.enabled = false;
                if (_frame) _frame.color = Color.white;
                if (_darkFrame) _darkFrame.enabled = false;
                if (_lockOverlay) _lockOverlay.SetActive(false);
                if (_checkMark) _checkMark.SetActive(true);
            }
            else if (isNext)
            {
                if (_button) _button.enabled = false;
                if (_frame) _frame.color = Color.gray;
                if (_darkFrame) _darkFrame.enabled = true;
                if (_lockOverlay) _lockOverlay.SetActive(true);
                if (_checkMark) _checkMark.SetActive(false);
            }
        }

        public void OnClick()
        {
            if (_geneticSystem == null || _stat == null)
                return;

            _geneticSystem.OpenPreview(_stat, _rectTransform.position);
        }
    }
}
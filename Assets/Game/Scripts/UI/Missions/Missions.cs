using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Missions
{
    public class Missions : Window
    {
        [Header("Зависимости")]
        [SerializeField] private TaskBar _barPrefab;
        [SerializeField] private TasksData _data;
        [SerializeField] private RectTransform _content;
        [SerializeField] private TMP_Text _titleTaskCount;

        private void Start()
        {
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            _titleTaskCount.text = _data.Tasks.Count.ToString();

            foreach (var task in _data.Tasks)
            {
                Instantiate(_barPrefab, _content).Init(task);
            }
        }

        protected override void Show()
        {
            _transition.Open(_canvasGroup, _rectTransform, _openButton.transform.position, _scaleEase, _positionEase,
                _duration);
        }

        protected override void Hide()
        {
            if (IsTransitionActive) return;

            if (_transition)
            {
                _transition.Close(_canvasGroup, _rectTransform);
            }
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Challenges
{
    public class Achievements : Window
    {
        [SerializeField] private AchievementData _data;
        [SerializeField] private AchieveBar _barPrefab;
        [SerializeField] private RectTransform _content;
        [SerializeField] private TMP_Text _titleAchievesCount;
        [SerializeField] private ScrollRect _scrollRect;

        private void Start()
        {
            InitializeAchieves();
        }

        private void InitializeAchieves()
        {
            _titleAchievesCount.text = _data.Achieves.Count.ToString();

            foreach (var achieve in _data.Achieves)
            {
                var bar = Instantiate(_barPrefab, _content);
                bar.Init(achieve, _scrollRect);
            }
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration
            );
        }

        protected override void Hide()
        {
            if (IsTransitionActive)
                return;

            if (_transition)
            {
                _transition.Close(
                    _canvasGroup,
                    _rectTransform
                );
            }
        }
    }
}
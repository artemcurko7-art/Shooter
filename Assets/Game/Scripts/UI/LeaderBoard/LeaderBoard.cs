using DG.Tweening;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.LeaderBoard
{
    public class LeaderBoard : Window
    {
        [Header("Зависимости")]
        [SerializeField] private LeaderboardYG _leaderboard;

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
using Game.Scripts.UI.Profile.Avatars;
using Game.Scripts.UI.Profile.Settings;
using Game.Scripts.UI.Profile.Statistics;
using UnityEngine;

namespace Game.Scripts.UI.Profile
{
    public class ProfileEditor : Window
    {
        [Header("Зависимости")]
        [SerializeField] private SettingsHandler _settingsHandler;
        [SerializeField] private AvatarsHandler _avatarsHandler;
        [SerializeField] private StatisticsHandler _statisticsHandler;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _statisticsHandler.Setup();
            _avatarsHandler.Setup();
            _settingsHandler.Setup();
        }

        
        protected override void Show()
        {
            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition);
        }

        protected override void Hide()
        {
            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Close(
                _canvasGroup,
                _rectTransform,
                FinishTransition);
        }
    }
}
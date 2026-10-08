using DG.Tweening;
using Game.Scripts.UI.Animation;
using Game.Scripts.UI.TabContext;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        [Header("Настройки перехода")]
        [SerializeField] protected Ease _scaleEase = Ease.OutExpo;
        [SerializeField] protected Ease _positionEase = Ease.OutExpo;
        [SerializeField] protected float _duration = 0.5f;

        [Header("Ссылки")]
        [SerializeField] protected RectTransform _rectTransform;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected WindowTransition _transition;
        [SerializeField] protected TabView _tabView;

        [Header("Кнопки")]
        [SerializeField] protected Button _openButton;
        [SerializeField] protected Button _exitButton;

        protected bool _isTransitionActive;

        protected virtual void OnEnable()
        {
            if (_openButton)
                _openButton.onClick.AddListener(OnOpenButtonClick);

            if (_exitButton)
                _exitButton.onClick.AddListener(OnExitButtonClick);

            if (_tabView)
                _tabView.Closed += OnViewClosed;
        }

        protected virtual void OnDisable()
        {
            if (_openButton)
                _openButton.onClick.RemoveListener(OnOpenButtonClick);

            if (_exitButton)
                _exitButton.onClick.RemoveListener(OnExitButtonClick);

            if (_tabView)
                _tabView.Closed -= OnViewClosed;
        }

        private void OnOpenButtonClick()
        {
            Show();
        }

        private void OnExitButtonClick()
        {
            Hide();
        }

        private void OnViewClosed()
        {
            QuickClose();
        }

        private void QuickClose()
        {
            if (!_transition || !_canvasGroup || !_rectTransform) return;

            _transition.QuickClose(_canvasGroup, _rectTransform);
        }

        protected abstract void Show();
        protected abstract void Hide();

        protected bool IsTransitionActive => _isTransitionActive;
    }
}
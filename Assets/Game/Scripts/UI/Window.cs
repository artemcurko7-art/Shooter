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

        private bool _isOpened;

        private bool IsTransitionActive { get; set; }

        protected virtual void Awake()
        {
            _isOpened = _rectTransform && _rectTransform.gameObject.activeSelf;
        }

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
            if (!TryBeginOpen())
                return;

            Show();
        }

        private void OnExitButtonClick()
        {
            if (!TryBeginClose())
                return;

            Hide();
        }

        protected bool TryBeginOpen()
        {
            if (_isOpened || IsTransitionActive)
                return false;

            _isOpened = true;
            IsTransitionActive = true;

            return true;
        }

        protected bool TryBeginClose()
        {
            if (!_isOpened || IsTransitionActive)
                return false;

            _isOpened = false;
            IsTransitionActive = true;

            return true;
        }

        protected void FinishTransition()
        {
            IsTransitionActive = false;
        }

        private void OnViewClosed()
        {
            QuickClose();
        }

        private void QuickClose()
        {
            if (!_transition || !_canvasGroup || !_rectTransform)
                return;

            _isOpened = false;
            IsTransitionActive = false;

            _transition.QuickClose(
                _canvasGroup,
                _rectTransform
            );
        }

        protected abstract void Show();
        protected abstract void Hide();
    }
}
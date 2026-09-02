using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        [Header("Настройки перехода")]
        [SerializeField] protected Ease _scaleEase = Ease.OutExpo;
        [SerializeField] protected Ease _positionEase = Ease.InExpo;
        [SerializeField] protected float _duration = 0.5f;
        [SerializeField] protected RectTransform _rectTransform;
        [SerializeField] protected CanvasGroup _canvasGroup;

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
        }

        protected virtual void OnDisable()
        {
            if (_openButton)
                _openButton.onClick.RemoveListener(OnOpenButtonClick);

            if (_exitButton)
                _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnOpenButtonClick()
        {
            Show();
        }

        private void OnExitButtonClick()
        {
            Hide();
        }

        protected abstract void Show();
        protected abstract void Hide();

        protected bool IsTransitionActive => _isTransitionActive;
    }
}

using DG.Tweening;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public abstract class Preview : MonoBehaviour
    {
        [Header("Настройки перехода")]
        [SerializeField] protected Ease _scaleEase = Ease.OutExpo;
        [SerializeField] protected Ease _positionEase = Ease.OutExpo;
        [SerializeField] protected float _duration = 0.5f;

        [Header("Ссылки")]
        [SerializeField] protected RectTransform _rectTransform;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected WindowTransition _transition;
        [SerializeField] protected Button _exitButton;

        private bool _isOpened;
        private bool _isTransitionActive;

        protected virtual void Awake()
        {
            _isOpened = _rectTransform && _rectTransform.gameObject.activeSelf;
        }

        protected virtual void OnEnable()
        {
            if (_exitButton != null)
                _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected virtual void OnDisable()
        {
            if (_exitButton != null)
                _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnExitButtonClick()
        {
            Debug.Log("[PreviewBase] Нажата кнопка закрытия (X).");
            ClosePreview(false);
        }

        protected void OpenPreview(Vector3 startPosition)
        {
            if (_isTransitionActive)
                return;

            _isOpened = true;
            _isTransitionActive = true;

            Show(startPosition);
        }

        protected void ClosePreview(bool playEffect = true)
        {
            if (!_isOpened || _isTransitionActive)
                return;

            Debug.Log("[PreviewBase] Запуск закрытия превью.");

            _isOpened = false;
            _isTransitionActive = true;

            Hide(playEffect);
        }

        protected void FinishTransition()
        {
            _isTransitionActive = false;
            Debug.Log("[PreviewBase] Анимация завершена, флаги сброшены.");
        }

        protected abstract void Show(Vector3 startPosition);
        protected abstract void Hide(bool playEffect);
    }
}

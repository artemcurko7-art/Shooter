using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Adventure
{
    public class CoolDownDisplayer : MonoBehaviour
    {
        private const float OVERSHOOT = 2f;
        private const float TIMER_UPDATE_INTERVAL = 1f;

        [SerializeField] private Transform _panelTransform;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private float _duration = 0.2f;

        [Header("Зависимости")]
        [SerializeField] private EnergyRestoreService _restoreService;

        private Tween _scaleTween;
        private Coroutine _timerCoroutine;
        private bool _isVisible;

        private void Awake()
        {
            if (!_panelTransform)
                _panelTransform = transform;

            _panelTransform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            if (!_restoreService)
                return;

            _restoreService.EnergyChanged += OnEnergyChanged;
            RefreshDisplay();
        }

        private void OnDisable()
        {
            if (_restoreService)
                _restoreService.EnergyChanged -= OnEnergyChanged;

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }

            _scaleTween?.Kill();
            _isVisible = false;
        }

        private void OnEnergyChanged(float energy)
        {
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            if (!_restoreService)
                return;

            bool shouldShow = _restoreService.CurrentEnergy < _restoreService.MaxEnergy;

            if (shouldShow)
            {
                if (!_isVisible)
                    ShowPanel();

                if (_timerCoroutine == null)
                    _timerCoroutine = StartCoroutine(UpdateTimerRoutine());
            }
            else
            {
                HidePanel();
            }
        }

        private IEnumerator UpdateTimerRoutine()
        {
            while (_restoreService &&
                   _restoreService.CurrentEnergy < _restoreService.MaxEnergy)
            {
                if (_timerText)
                    _timerText.text = _restoreService.GetTimerText();

                yield return new WaitForSecondsRealtime(TIMER_UPDATE_INTERVAL);
            }

            if (_timerText)
                _timerText.text = "00:00";

            _timerCoroutine = null;
            RefreshDisplay();
        }

        private void ShowPanel()
        {
            _isVisible = true;
            _scaleTween?.Kill();

            _panelTransform.localScale = Vector3.zero;

            _scaleTween = _panelTransform
                .DOScale(Vector3.one, _duration)
                .SetEase(Ease.OutBack, OVERSHOOT);
        }

        private void HidePanel()
        {
            if (!_isVisible)
                return;

            _isVisible = false;
            _scaleTween?.Kill();

            _scaleTween = _panelTransform
                .DOScale(Vector3.zero, _duration)
                .SetEase(Ease.InBack, OVERSHOOT);

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
        }
    }
}
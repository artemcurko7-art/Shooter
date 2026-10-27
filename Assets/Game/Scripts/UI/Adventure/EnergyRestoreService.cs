
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Adventure
{
    public class EnergyRestoreService : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _maxEnergy = 5;
        [SerializeField, Min(1f)] private float _restoreIntervalSeconds = 600f;

        public event Action EnergyRestored;
        public event Action<float> EnergyChanged;

        public int MaxEnergy => _maxEnergy;
        public int CurrentEnergy => YG2.saves.Energy;
        public bool IsRestoring => _restoreCoroutine != null;

        private DateTime? _nextRestoreTimeUtc;
        private Coroutine _restoreCoroutine;
        private bool _isInitialized;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            YG2.saves.Energy = Mathf.Clamp(YG2.saves.Energy, 0, _maxEnergy);

            string savedTime = YG2.saves.NextEnergyRestoreTime;

            if (!string.IsNullOrEmpty(savedTime) &&
                DateTime.TryParse(
                    savedTime,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out DateTime parsedTime))
            {
                _nextRestoreTimeUtc = parsedTime.ToUniversalTime();
            }
            else
            {
                _nextRestoreTimeUtc = null;
            }

            _isInitialized = true;

            if (YG2.saves.Energy < _maxEnergy)
            {
                if (!_nextRestoreTimeUtc.HasValue)
                    SetNextRestoreTime();

                ProcessOfflineRestores();

                if (YG2.saves.Energy < _maxEnergy)
                    StartRestoreCoroutine();
            }
            else
            {
                _nextRestoreTimeUtc = null;
                SaveProgress();
            }

            EnergyChanged?.Invoke(YG2.saves.Energy);
        }

        public void OnEnergySpent()
        {
            if (!_isInitialized || YG2.saves.Energy >= _maxEnergy)
                return;

            if (!_nextRestoreTimeUtc.HasValue)
                SetNextRestoreTime();

            SaveProgress();
            StartRestoreCoroutine();
            EnergyChanged?.Invoke(YG2.saves.Energy);
        }

        private void SetNextRestoreTime()
        {
            _nextRestoreTimeUtc = DateTime.UtcNow.AddSeconds(_restoreIntervalSeconds);
            SaveTime();
        }

        private void ProcessOfflineRestores()
        {
            if (!_nextRestoreTimeUtc.HasValue || YG2.saves.Energy >= _maxEnergy)
                return;

            DateTime now = DateTime.UtcNow;

            if (now < _nextRestoreTimeUtc.Value)
                return;

            double elapsed = (now - _nextRestoreTimeUtc.Value).TotalSeconds;
            int restoreCount = 1 + (int)(elapsed / _restoreIntervalSeconds);
            int energyToAdd = Mathf.Min(restoreCount, _maxEnergy - YG2.saves.Energy);

            YG2.saves.Energy += energyToAdd;

            for (int i = 0; i < energyToAdd; i++)
                EnergyRestored?.Invoke();

            if (YG2.saves.Energy >= _maxEnergy)
            {
                _nextRestoreTimeUtc = null;
            }
            else
            {
                _nextRestoreTimeUtc = _nextRestoreTimeUtc.Value.AddSeconds(
                    restoreCount * _restoreIntervalSeconds);
            }

            SaveProgress();
            EnergyChanged?.Invoke(YG2.saves.Energy);
        }

        private void StartRestoreCoroutine()
        {
            if (!_isInitialized || !isActiveAndEnabled)
                return;

            if (YG2.saves.Energy >= _maxEnergy)
            {
                StopRestoreCoroutine();
                _nextRestoreTimeUtc = null;
                SaveProgress();
                return;
            }

            if (!_nextRestoreTimeUtc.HasValue)
                SetNextRestoreTime();

            if (_restoreCoroutine == null)
                _restoreCoroutine = StartCoroutine(RestoreRoutine());
        }

        private IEnumerator RestoreRoutine()
        {
            while (YG2.saves.Energy < _maxEnergy)
            {
                if (!_nextRestoreTimeUtc.HasValue)
                    SetNextRestoreTime();

                double remaining = (
                    _nextRestoreTimeUtc.Value - DateTime.UtcNow).TotalSeconds;

                if (remaining > 0)
                {
                    yield return new WaitForSecondsRealtime((float)remaining);
                    continue;
                }

                YG2.saves.Energy = Mathf.Min(YG2.saves.Energy + 1, _maxEnergy);

                EnergyRestored?.Invoke();
                EnergyChanged?.Invoke(YG2.saves.Energy);

                if (YG2.saves.Energy >= _maxEnergy)
                {
                    _nextRestoreTimeUtc = null;
                    SaveProgress();
                    _restoreCoroutine = null;
                    yield break;
                }

                _nextRestoreTimeUtc = _nextRestoreTimeUtc.Value.AddSeconds(
                    _restoreIntervalSeconds);

                SaveProgress();
            }

            _restoreCoroutine = null;
        }

        public string GetTimerText()
        {
            if (!_isInitialized ||
                YG2.saves.Energy >= _maxEnergy ||
                !_nextRestoreTimeUtc.HasValue)
            {
                return string.Empty;
            }

            TimeSpan remaining = _nextRestoreTimeUtc.Value - DateTime.UtcNow;

            if (remaining <= TimeSpan.Zero)
                return "00:00";

            return remaining.TotalHours >= 1
                ? $"{(int)remaining.TotalHours:00}:{remaining.Minutes:00}:{remaining.Seconds:00}"
                : $"{remaining.Minutes:00}:{remaining.Seconds:00}";
        }

        private void SaveTime()
        {
            YG2.saves.NextEnergyRestoreTime =
                _nextRestoreTimeUtc?.ToString("o", CultureInfo.InvariantCulture)
                ?? string.Empty;
        }

        private void SaveProgress()
        {
            if (!_isInitialized)
                return;

            if (YG2.saves.Energy >= _maxEnergy)
            {
                _nextRestoreTimeUtc = null;
            }
            else if (!_nextRestoreTimeUtc.HasValue)
            {
                _nextRestoreTimeUtc = DateTime.UtcNow.AddSeconds(_restoreIntervalSeconds);
            }

            SaveTime();
            YG2.SaveProgress();
        }

        private void StopRestoreCoroutine()
        {
            if (_restoreCoroutine == null)
                return;

            StopCoroutine(_restoreCoroutine);
            _restoreCoroutine = null;
        }

        private void OnDisable()
        {
            if (_isInitialized)
                SaveProgress();

            StopRestoreCoroutine();
        }

        private void OnEnable()
        {
            if (!_isInitialized || !Application.isPlaying)
                return;

            if (YG2.saves.Energy < _maxEnergy)
                StartRestoreCoroutine();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus && _isInitialized)
                SaveProgress();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && _isInitialized)
                SaveProgress();
        }

        private void OnApplicationQuit()
        {
            if (_isInitialized)
                SaveProgress();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        private readonly List<StatBar> _statBars = new();
        private readonly List<GameObject> _activeWires = new();

        [SerializeField] private float _statIncreaseNumber = 0.5f;
        [SerializeField] private int _additionallyStatVisibleCount = 5;
        [SerializeField] private float _uvSpeed = 2000f;
        [SerializeField] private float _scrollDuration = 0.5f;

        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Preview _preview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _gridContainer;
        [SerializeField] private RawImage _background;

        [Header("Провода")]
        [SerializeField] private GameObject _wirePrefab;
        [SerializeField] private Transform _wiresContainer;
        [SerializeField] private float _wireThickness = 4f;
        [SerializeField] private Color _wireUnlockedColor = new Color(0.2f, 0.9f, 0.4f, 1f);
        [SerializeField] private Color _wireNextColor = new Color(0.9f, 0.8f, 0.2f, 1f);
        [SerializeField] private Color _wireLockedColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        [SerializeField] private float _pulseSpeed = 3f;

        private Coroutine _scrollCoroutine;
        private Camera _canvasCamera;
        private bool _initialized;
        private float _pulseTimer;

        public float IncreaseNumber => _statIncreaseNumber;

        private void OnEnable()
        {
            StartCoroutine(CheckScrollOnEnable());
        }

        private void Start()
        {
            CacheCanvasCamera();
            InitializeStats();
            ScrollToNextAvailable(false);
            UpdateWires();
        }

        private void OnDisable()
        {
            if (_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            _statBars.Clear();
            ClearWires();
        }

        private void LateUpdate()
        {
            if (!_background || !_gridContainer) return;

            var rect = _background.uvRect;
            rect.y = _gridContainer.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;

            if (_activeWires.Count > 0)
            {
                _pulseTimer += Time.deltaTime * _pulseSpeed;
                float pulse = (Mathf.Sin(_pulseTimer) + 1f) * 0.5f;
                int unlockedCount = YG2.saves.IdSavedStatCount;

                for (int i = 0; i < _activeWires.Count; i++)
                {
                    var wireObj = _activeWires[i];
                    if (!wireObj) continue;

                    var img = wireObj.GetComponent<Image>();
                    if (!img) continue;

                    if (i < unlockedCount)
                    {
                        float p = (Mathf.Sin(_pulseTimer + i * 0.3f) + 1f) * 0.5f;
                        img.color = Color.Lerp(_wireUnlockedColor * 0.6f, _wireUnlockedColor, p);
                    }
                    else if (i == unlockedCount)
                    {
                        float p = (Mathf.Sin(_pulseTimer * 1.5f) + 1f) * 0.5f;
                        img.color = Color.Lerp(_wireNextColor * 0.5f, _wireNextColor, p);
                    }
                }
            }
        }

        public static bool IsNextAvailableStat(int statId)
        {
            return statId == YG2.saves.IdSavedStatCount;
        }

        public static bool IsAlreadyUnlocked(int statId)
        {
            return statId < YG2.saves.IdSavedStatCount;
        }

        public float GetStatValue(string statName)
        {
            return statName switch
            {
                "AttackStrength" => YG2.saves.AttackStrength,
                "CriticalDamage" => YG2.saves.CriticalDamage,
                "Armor" => YG2.saves.Armor,
                "MovementSpeed" => YG2.saves.MovementSpeed,
                "ViewRange" => YG2.saves.ViewRange,
                _ => 0
            };
        }

        public void IncreaseStat(string statName)
        {
            switch (statName)
            {
                case "AttackStrength":
                    YG2.saves.AttackStrength += _statIncreaseNumber;
                    break;
                case "CriticalDamage":
                    YG2.saves.CriticalDamage += _statIncreaseNumber;
                    break;
                case "Armor":
                    YG2.saves.Armor += _statIncreaseNumber;
                    break;
                case "MovementSpeed":
                    YG2.saves.MovementSpeed += _statIncreaseNumber;
                    break;
                case "ViewRange":
                    YG2.saves.ViewRange += _statIncreaseNumber;
                    break;
                default:
                    return;
            }

            YG2.SaveProgress();
            EnsureVisibleRange();
            ScrollToNextAvailable(true);
            UpdateWires();
        }

        public void OpenPreview(StatsData.Stat stat, RectTransform statPosition)
        {
            if (!_preview) return;
            _preview.gameObject.SetActive(true);
            _preview.Open(stat, statPosition);
        }

        private void CacheCanvasCamera()
        {
            var canvas = _scrollRect.GetComponentInParent<Canvas>();
            _canvasCamera = canvas ? canvas.worldCamera : null;
        }

        private IEnumerator CheckScrollOnEnable()
        {
            if (!_initialized) yield break;

            yield return null;
            yield return new WaitForEndOfFrame();

            EnsureLayout();
            ScrollToNextAvailable(true);
            UpdateWires();
        }

        private void InitializeStats()
        {
            if (!_statsData || _statsData.Stats.Count == 0)
            {
                Debug.LogError("[GeneticSystem] StatsData не назначен или список статов пуст!");
                return;
            }

            _statBars.Clear();

            var unlockedCount = YG2.saves.IdSavedStatCount;
            var totalBars = unlockedCount + _additionallyStatVisibleCount;

            for (var i = 0; i < totalBars; i++)
            {
                var statIndexInList = i % _statsData.Stats.Count;
                var stat = _statsData.Stats[statIndexInList];
                var statBar = Instantiate(_statBarPrefab, _gridContainer);

                statBar.Init(this, stat, i);
                _statBars.Add(statBar);
            }

            EnsureLayout();
            RefreshUI();
            _initialized = true;
        }

        private void EnsureVisibleRange()
        {
            if (!_statsData || _statsData.Stats.Count == 0) return;

            var unlockedCount = YG2.saves.IdSavedStatCount;
            var totalNeeded = unlockedCount + _additionallyStatVisibleCount;

            while (_statBars.Count < totalNeeded)
            {
                var i = _statBars.Count;
                var statIndexInList = i % _statsData.Stats.Count;
                var stat = _statsData.Stats[statIndexInList];

                var statBar = Instantiate(_statBarPrefab, _gridContainer);
                statBar.Init(this, stat, i);

                _statBars.Add(statBar);
            }

            EnsureLayout();
            RefreshUI();
        }

        private void EnsureLayout()
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_gridContainer);
            Canvas.ForceUpdateCanvases();
        }

        private void RefreshUI()
        {
            foreach (var bar in _statBars)
            {
                bar.UpdateDisplay();
            }
        }

        private void ScrollToNextAvailable(bool animated)
        {
            if (!_gridContainer || !_scrollRect) return;

            var nextStatIndex = YG2.saves.IdSavedStatCount;
            if (nextStatIndex >= _statBars.Count) return;

            var statTransform = _statBars[nextStatIndex].GetComponent<RectTransform>();
            if (!statTransform) return;

            EnsureLayout();

            var contentRect = _scrollRect.content;
            var viewportRect = _scrollRect.viewport;
            if (!contentRect || !viewportRect) return;

            var corners = new Vector3[4];
            statTransform.GetWorldCorners(corners);
            var elementCenter = (corners[0] + corners[2]) * 0.5f;

            var screenPoint = RectTransformUtility.WorldToScreenPoint(_canvasCamera, elementCenter);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewportRect, screenPoint, _canvasCamera, out var viewportPoint))
            {
                return;
            }

            var deltaY = viewportPoint.y - viewportRect.rect.center.y;
            var targetY = contentRect.anchoredPosition.y - deltaY;
            targetY = GetClampedScrollY(contentRect, targetY);

            if (_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }

            if (!animated)
            {
                contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, targetY);
                return;
            }

            _scrollCoroutine = StartCoroutine(SmoothScroll(contentRect, targetY, _scrollDuration));
        }

        private IEnumerator SmoothScroll(RectTransform content, float targetY, float duration)
        {
            var start = content.anchoredPosition;

            if (duration <= 0f)
            {
                content.anchoredPosition = new Vector2(start.x, targetY);
                _scrollCoroutine = null;
                yield break;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                t = Mathf.SmoothStep(0f, 1f, t);
                content.anchoredPosition = new Vector2(start.x, Mathf.Lerp(start.y, targetY, t));
                yield return null;
            }

            content.anchoredPosition = new Vector2(start.x, targetY);
            _scrollCoroutine = null;
        }

        private float GetClampedScrollY(RectTransform content, float targetY)
        {
            var contentHeight = content.rect.height;
            var viewportHeight = _scrollRect.viewport.rect.height;

            if (contentHeight <= viewportHeight)
                return content.anchoredPosition.y;

            const float maxY = 0f;
            var minY = -(contentHeight - viewportHeight);

            return Mathf.Clamp(targetY, minY, maxY);
        }

        private void UpdateWires()
        {
            ClearWires();

            if (_statBars.Count < 2 || !_wirePrefab || !_wiresContainer) return;

            EnsureLayout();

            int unlockedCount = YG2.saves.IdSavedStatCount;

            for (int i = 0; i < _statBars.Count - 1; i++)
            {
                var startRect = _statBars[i].GetComponent<RectTransform>();
                var endRect = _statBars[i + 1].GetComponent<RectTransform>();

                if (!startRect || !endRect) continue;

                var startPos = startRect.position;
                var endPos = endRect.position;

                var wireObj = Instantiate(_wirePrefab, _wiresContainer);
                _activeWires.Add(wireObj);

                var wireRect = wireObj.GetComponent<RectTransform>();
                var wireImage = wireObj.GetComponent<Image>();

                if (!wireRect || !wireImage) continue;

                var diff = endPos - startPos;
                var length = diff.magnitude;
                var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                wireRect.position = startPos;
                wireRect.sizeDelta = new Vector2(length, _wireThickness);
                wireRect.localEulerAngles = new Vector3(0, 0, angle);
                wireRect.pivot = new Vector2(0, 0.5f);

                if (i < unlockedCount)
                {
                    wireImage.color = _wireUnlockedColor;
                }
                else if (i == unlockedCount)
                {
                    wireImage.color = _wireNextColor;
                }
                else
                {
                    wireImage.color = _wireLockedColor;
                }
            }
        }

        private void ClearWires()
        {
            foreach (var wire in _activeWires)
            {
                if (wire) Destroy(wire);
            }
            _activeWires.Clear();
        }
    }
}

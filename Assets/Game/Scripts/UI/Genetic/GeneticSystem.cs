using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Genetic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        private readonly List<StatBar> _statBars = new();

        [SerializeField] private float _statIncreaseNumber = 0.5f;
        [SerializeField] private int _poolSize = 20; // Сколько всего баров держим в пуле (последние 20 штук)
        [SerializeField] private int _additionallyStatVisibleCount = 5;
        [SerializeField] private float _uvSpeed = 2000f;

        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private SmoothScroll _smoothScroll;
        [SerializeField] private GeneticPreview _geneticPreview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _gridContainer;
        [SerializeField] private RawImage _background;

        public float IncreaseNumber => _statIncreaseNumber;

        private bool _isInitialized;

        private void Start()
        {
            TryInitializeOnce();
        }

        private void OnEnable()
        {
            if (!_isInitialized)
            {
                TryInitializeOnce();
            }
            else
            {
                RefreshUI();
                StartCoroutine(CheckScrollOnEnable());
            }
        }

        private void TryInitializeOnce()
        {
            if (_isInitialized) return;
            InitializePool();
            _isInitialized = true;
            StartCoroutine(CheckScrollOnEnable());
        }

        public static bool IsNextStat(int statId)
        {
            var nextIndex = YG2.saves.IdSavedStatCount;
            return statId > nextIndex;
        }

        public static bool IsAvailableStat(int statId)
        {
            var nextIndex = YG2.saves.IdSavedStatCount;
            return statId == nextIndex;
        }

        public static bool IsAlreadyUnlocked(int statId)
        {
            var nextIndex = YG2.saves.IdSavedStatCount;
            return statId < nextIndex;
        }

        public static float GetStatValue(string statName)
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
        }

        public void OpenPreview(StatsData.Stat stat, Vector3 statPosition)
        {
            _geneticPreview.gameObject.SetActive(true);
            _geneticPreview.Open(stat, statPosition);
        }

        private IEnumerator CheckScrollOnEnable()
        {
            yield return null;
            yield return new WaitForEndOfFrame();

            EnsureLayout();
            ScrollToNextAvailable(false);
        }

        private void LateUpdate()
        {
            if (!_background || !_gridContainer)
                return;

            var rect = _background.uvRect;
            rect.y = _gridContainer.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;
        }

        private void InitializePool()
        {
            if (!_statsData || _statsData.Stats.Count == 0)
            {
                Debug.LogError("[GeneticSystem] StatsData не назначен или список статов пуст!");
                return;
            }

            _statBars.Clear();

            var currentUnlocked = YG2.saves.IdSavedStatCount;
            var startIndex = Mathf.Max(0, currentUnlocked - _poolSize + _additionallyStatVisibleCount);
            var totalToCreate = Mathf.Max(_poolSize, currentUnlocked + _additionallyStatVisibleCount);

            for (var i = startIndex; i < totalToCreate; i++)
            {
                var statIndex = i % _statsData.Stats.Count;
                var statBar = Instantiate(_statBarPrefab, _gridContainer);

                statBar.Init(this, _statsData.Stats[statIndex], i);
                _statBars.Add(statBar);
            }

            RefreshUI();

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_gridContainer);
        }

        private void EnsureVisibleRange()
        {
            if (!_statsData || _statsData.Stats.Count == 0)
                return;

            var currentUnlocked = YG2.saves.IdSavedStatCount;
            var maxIndexInPool = _statBars.Count > 0 ? _statBars[_statBars.Count - 1].Index : 0;
            var layoutChanged = false;

            while (currentUnlocked + _additionallyStatVisibleCount > maxIndexInPool)
            {
                var newIndex = maxIndexInPool + 1;
                var statIndex = newIndex % _statsData.Stats.Count;

                var statBar = Instantiate(_statBarPrefab, _gridContainer);
                statBar.Init(this, _statsData.Stats[statIndex], newIndex);
                _statBars.Add(statBar);

                maxIndexInPool = newIndex;
                layoutChanged = true;
            }

            if (layoutChanged)
            {
                Canvas.ForceUpdateCanvases();
                LayoutRebuilder.ForceRebuildLayoutImmediate(_gridContainer);
            }

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
                bar.UpdateDisplay();
        }

        private void ScrollToNextAvailable(bool animated)
        {
            if (!_gridContainer || !_scrollRect)
                return;

            var nextStatIndex = YG2.saves.IdSavedStatCount;

            var targetBar = _statBars.Find(b => b.Index == nextStatIndex);
            if (!targetBar) return;

            var statTransform = targetBar.transform as RectTransform;
            if (!statTransform) return;

            var contentRect = _scrollRect.content;
            var viewportRect = _scrollRect.viewport;

            var corners = new Vector3[4];
            statTransform.GetWorldCorners(corners);

            var elementCenter = (corners[0] + corners[2]) * 0.5f;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewportRect,
                    RectTransformUtility.WorldToScreenPoint(null, elementCenter),
                    null,
                    out var viewportPoint))
            {
                return;
            }

            var deltaY = viewportPoint.y - viewportRect.rect.center.y;

            if (Mathf.Abs(deltaY) < 1f)
                return;

            var targetY = contentRect.anchoredPosition.y - deltaY;

            targetY = GetClampedScrollY(contentRect, targetY);

            if (animated)
            {
                if (_smoothScroll)
                    _smoothScroll.ScrollToY(targetY);
                else
                    contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, targetY);

                return;
            }

            if (_smoothScroll)
                _smoothScroll.SetPositionY(targetY);
            else
                contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, targetY);
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
    }
}
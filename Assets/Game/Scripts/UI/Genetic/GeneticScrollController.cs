using System.Collections;
using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticScrollController : MonoBehaviour
    {
        private readonly List<StatBar> _statBars = new();

        [Header("Настройки характеристик")]
        [SerializeField, Min(1)] private int _poolSize = 20;
        [SerializeField, Min(1)] private int _additionallyStatVisibleCount = 5;

        [Header("Зависимости")]
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _statContainer;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private SmoothScroll _smoothScroll;
        [SerializeField] private GeneticSystem _geneticSystem;

        private bool _isInitialized;
        private Coroutine _scrollCoroutine;

        public IReadOnlyList<StatBar> StatBars => _statBars;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            if (!ValidateDependencies())
                return;

            InitializePool();

            if (_statBars.Count == 0)
                return;

            _isInitialized = true;
            RefreshAndScroll(false);
        }

        public void RefreshAndScroll(bool animated)
        {
            RefreshUI();

            if (_scrollCoroutine != null)
                StopCoroutine(_scrollCoroutine);

            _scrollCoroutine = StartCoroutine(CheckScrollOnEnable(animated));
        }

        public void RefreshUI()
        {
            foreach (var bar in _statBars)
            {
                if (bar)
                    bar.Refresh();
            }
        }

        public void EnsureVisibleRange()
        {
            if (!_statsData || _statsData.Stats == null || _statsData.Stats.Count == 0)
                return;

            var currentUnlocked = YG2.saves.IdSavedStatCount;
            var maxIndexInPool = _statBars.Count > 0
                ? _statBars[_statBars.Count - 1].Index
                : -1;

            var layoutChanged = false;

            while (currentUnlocked + _additionallyStatVisibleCount > maxIndexInPool)
            {
                var newIndex = maxIndexInPool + 1;
                var statIndex = newIndex % _statsData.Stats.Count;

                var statBar = Instantiate(_statBarPrefab, _statContainer);
                statBar.Init(_geneticSystem, _statsData.Stats[statIndex], newIndex);
                _statBars.Add(statBar);

                maxIndexInPool = newIndex;
                layoutChanged = true;
            }

            if (layoutChanged)
                EnsureLayout();

            RefreshUI();
        }

        private bool ValidateDependencies()
        {
            if (_statsData && _statBarPrefab && _statContainer && _scrollRect &&
                _scrollRect.content && _scrollRect.viewport && _geneticSystem)
            {
                return true;
            }

            Debug.LogError(
                "[GeneticScrollController] Не назначены необходимые зависимости!",
                this);

            return false;
        }

        private void InitializePool()
        {
            if (_statsData.Stats.Count == 0)
            {
                Debug.LogError("[GeneticScrollController] Список характеристик пуст!", this);
                return;
            }

            _statBars.Clear();

            var currentUnlocked = YG2.saves.IdSavedStatCount;
            var startIndex = Mathf.Max(
                0,
                currentUnlocked - _poolSize + _additionallyStatVisibleCount);

            var totalToCreate = Mathf.Max(
                _poolSize,
                currentUnlocked + _additionallyStatVisibleCount);

            for (var i = startIndex; i < totalToCreate; i++)
            {
                var statIndex = i % _statsData.Stats.Count;
                var statBar = Instantiate(_statBarPrefab, _statContainer);

                statBar.Init(_geneticSystem, _statsData.Stats[statIndex], i);
                _statBars.Add(statBar);
            }

            RefreshUI();
            EnsureLayout();
        }

        private IEnumerator CheckScrollOnEnable(bool animated)
        {
            yield return null;
            yield return new WaitForEndOfFrame();

            EnsureVisibleRange();
            EnsureLayout();
            ScrollToNextAvailable(animated);

            _scrollCoroutine = null;
        }

        private void EnsureLayout()
        {
            if (_statContainer)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_statContainer);

            if (_scrollRect && _scrollRect.content)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);

            Canvas.ForceUpdateCanvases();
        }

        private void ScrollToNextAvailable(bool animated)
        {
            if (!_statContainer || !_scrollRect || !_scrollRect.content || !_scrollRect.viewport)
                return;

            var nextStatIndex = YG2.saves.IdSavedStatCount;
            var targetBar = _statBars.Find(bar => bar && bar.Index == nextStatIndex);

            if (!targetBar)
                return;

            var statTransform = targetBar.transform as RectTransform;

            if (!statTransform)
                return;

            var contentRect = _scrollRect.content;
            var viewportRect = _scrollRect.viewport;

            var corners = new Vector3[4];
            statTransform.GetWorldCorners(corners);

            var elementCenter = (corners[0] + corners[2]) * 0.5f;
            var screenPoint = RectTransformUtility.WorldToScreenPoint(null, elementCenter);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewportRect,
                    screenPoint,
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
                    contentRect.anchoredPosition =
                        new Vector2(contentRect.anchoredPosition.x, targetY);

                return;
            }

            if (_smoothScroll)
                _smoothScroll.SetPositionY(targetY);
            else
                contentRect.anchoredPosition =
                    new Vector2(contentRect.anchoredPosition.x, targetY);
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
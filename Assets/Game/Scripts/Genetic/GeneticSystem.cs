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

        [SerializeField] private float _statIncreaseNumber = 0.5f;
        [SerializeField] private int _additionallyStatVisibleCount = 5;
        [SerializeField] private float _uvSpeed = 2000f;

        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Preview _preview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _gridContainer;
        [SerializeField] private RawImage _background;
        [SerializeField] private float rectScroll = 0.5f;

        private Coroutine _scrollCoroutine;

        public float IncreaseNumber => _statIncreaseNumber;

        private void OnEnable()
        {
            StartCoroutine(CheckScrollOnEnable());
        }

        private void Start()
        {
            InitializeStats();
            ScrollToNextAvailable(false);
        }

        private IEnumerator CheckScrollOnEnable()
        {
            yield return null;
            yield return new WaitForEndOfFrame();

            EnsureLayout();
            ScrollToNextAvailable(true);
        }

        private void LateUpdate()
        {
            if (!_background || !_gridContainer) return;

            var rect = _background.uvRect;
            rect.y = _gridContainer.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;
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

        public static bool IsNextAvailableStat(int statId)
        {
            var nextIndex = YG2.saves.IdSavedStatCount;
            return statId == nextIndex;
        }

        public static bool IsAlreadyUnlocked(int statId)
        {
            var nextIndex = YG2.saves.IdSavedStatCount;
            return statId < nextIndex;
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

        public void OpenPreview(StatsData.Stat stat, RectTransform statPosition)
        {
            _preview.gameObject.SetActive(true);
            _preview.Open(stat, statPosition);
        }

        private void ScrollToNextAvailable(bool animated)
        {
            if (!_gridContainer || !_scrollRect) return;

            int nextStatIndex = YG2.saves.IdSavedStatCount;

            if (nextStatIndex >= _statBars.Count)
                return;

            RectTransform statTransform =
                _statBars[nextStatIndex].GetComponent<RectTransform>();

            if (!statTransform)
                return;

            EnsureLayout();

            RectTransform contentRect = _scrollRect.content;
            RectTransform viewportRect = _scrollRect.viewport;

            Vector3[] corners = new Vector3[4];
            statTransform.GetWorldCorners(corners);

            Vector3 elementCenter =
                (corners[0] + corners[2]) * 0.5f;

            Vector2 viewportPoint;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewportRect,
                    RectTransformUtility.WorldToScreenPoint(null, elementCenter),
                    null,
                    out viewportPoint))
            {
                return;
            }

            float deltaY =
                viewportPoint.y - viewportRect.rect.center.y;

            if (Mathf.Abs(deltaY) < 1f)
                return;

            float targetY =
                contentRect.anchoredPosition.y - deltaY;

            targetY = GetClampedScrollY(contentRect, targetY);

            if (_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }

            if (!animated)
            {
                contentRect.anchoredPosition =
                    new Vector2(
                        contentRect.anchoredPosition.x,
                        targetY
                    );

                return;
            }

            _scrollCoroutine =
                StartCoroutine(
                    SmoothScroll(
                        contentRect,
                        targetY,
                        rectScroll
                    )
                );
        }

        private IEnumerator SmoothScroll(
            RectTransform content,
            float targetY,
            float duration)
        {
            Vector2 start = content.anchoredPosition;

            if (duration <= 0f)
            {
                content.anchoredPosition =
                    new Vector2(start.x, targetY);

                _scrollCoroutine = null;
                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;

                float t =
                    Mathf.Clamp01(elapsed / duration);

                t = Mathf.SmoothStep(0f, 1f, t);

                content.anchoredPosition =
                    new Vector2(
                        start.x,
                        Mathf.Lerp(
                            start.y,
                            targetY,
                            t
                        )
                    );

                yield return null;
            }

            content.anchoredPosition =
                new Vector2(start.x, targetY);

            _scrollCoroutine = null;
        }

        private float GetClampedScrollY(
            RectTransform content,
            float targetY)
        {
            float contentHeight = content.rect.height;
            float viewportHeight = _scrollRect.viewport.rect.height;

            if (contentHeight <= viewportHeight)
                return content.anchoredPosition.y;

            float maxY = 0f;
            float minY = -(contentHeight - viewportHeight);

            return Mathf.Clamp(
                targetY,
                minY,
                maxY
            );
        }
    }
}

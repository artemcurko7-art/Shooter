using System.Collections;
using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticSystem : MonoBehaviour
    {
        private readonly List<StatBar> _statBars = new();
        private readonly List<AdditionBar> _additionBars = new();

        [Header("Настройки характеристик")]
        [SerializeField] private float _statIncreaseNumber = 0.5f;
        [SerializeField, Min(1)] private int _poolSize = 20;
        [SerializeField, Min(1)] private int _additionallyStatVisibleCount = 5;

        [Header("Дополнительные награды")]
        [SerializeField, Min(1)] private int _initialAdditionsCount = 5;
        [SerializeField, Min(1)] private int _statUpgradesPerAddition = 3;
        [SerializeField] private AdditionData _additionData;
        [SerializeField] private AdditionBar _additionBarPrefab;

        [Header("Зависимости")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private SmoothScroll _smoothScroll;
        [SerializeField] private GeneticPreview _geneticPreview;
        [SerializeField] private StatsData _statsData;
        [SerializeField] private StatBar _statBarPrefab;
        [SerializeField] private RectTransform _statContainer;
        [SerializeField] private RectTransform _additionsContainer;

        [Header("Задний фон")]
        [SerializeField] private float _uvSpeed = 2000f;
        [SerializeField] private RawImage _background;
        [SerializeField] private RectTransform _content;

        public float IncreaseNumber => _statIncreaseNumber;

        private bool _isInitialized;
        private int _lastAdditionUpgradeCount = -1;

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

        private void LateUpdate()
        {
            if (!_background || !_statContainer || !_content || _uvSpeed <= 0f)
                return;

            var rect = _background.uvRect;
            rect.y = _content.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;
        }

        private void TryInitializeOnce()
        {
            if (_isInitialized)
                return;

            InitializePool();

            if (_statBars.Count == 0)
                return;

            InitializeAdditions();

            _isInitialized = true;
            StartCoroutine(CheckScrollOnEnable());
        }

        public static bool IsNextStat(int statId)
        {
            return statId > YG2.saves.IdSavedStatCount;
        }

        public static bool IsAvailableStat(int statId)
        {
            return statId == YG2.saves.IdSavedStatCount;
        }

        public static bool IsAlreadyUnlocked(int statId)
        {
            return statId < YG2.saves.IdSavedStatCount;
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
                _ => 0f
            };
        }

        public static float GetStatValue(StatsData.Stat stat)
        {
            return stat == null ? 0f : GetStatValue(stat.name);
        }

        public void IncreaseStat(string statName)
        {
            Debug.Log(
                $"[GeneticSystem] Начало прокачки: {statName}, " +
                $"IdSavedStatCount = {YG2.saves.IdSavedStatCount}"
            );

            float valueBefore = GetStatValue(statName);

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
                    Debug.LogWarning(
                        $"[GeneticSystem] Неизвестная характеристика: {statName}",
                        this
                    );
                    return;
            }

            float valueAfter = GetStatValue(statName);

            Debug.Log(
                $"[GeneticSystem] {statName}: {valueBefore} -> {valueAfter}, " +
                $"прирост = {valueAfter - valueBefore}"
            );

            int indexBefore = YG2.saves.IdSavedStatCount;
            YG2.saves.IdSavedStatCount++;

            Debug.Log(
                $"[GeneticSystem] Индекс: {indexBefore} -> {YG2.saves.IdSavedStatCount}"
            );

            TryAddAddition();

            YG2.SaveProgress();

            EnsureVisibleRange();
            ScrollToNextAvailable(true);
        }

        public void IncreaseStat(StatsData.Stat stat, int index)
        {
            Debug.Log(
                $"[GeneticSystem] IncreaseStat(stat): {stat?.name}, index = {index}, " +
                $"IdSavedStatCount = {YG2.saves.IdSavedStatCount}"
            );

            if (stat == null || !IsAvailableStat(index))
                return;

            IncreaseStat(stat.name);
        }

        public void OpenPreview(StatsData.Stat stat, int index, Vector3 statPosition)
        {
            if (stat == null || !_geneticPreview)
                return;

            _geneticPreview.gameObject.SetActive(true);
            _geneticPreview.Open(stat, index, statPosition);
        }

        private IEnumerator CheckScrollOnEnable()
        {
            yield return null;
            yield return new WaitForEndOfFrame();

            EnsureLayout();
            ScrollToNextAvailable(false);
        }

        private void InitializePool()
        {
            if (!_statsData || _statsData.Stats == null || _statsData.Stats.Count == 0)
            {
                Debug.LogError(
                    "[GeneticSystem] StatsData не назначен или список статов пуст!",
                    this
                );
                return;
            }

            if (!_statBarPrefab || !_statContainer)
            {
                Debug.LogError(
                    "[GeneticSystem] Не назначены StatBar Prefab или Stats Container!",
                    this
                );
                return;
            }

            _statBars.Clear();

            var currentUnlocked = YG2.saves.IdSavedStatCount;
            var startIndex = Mathf.Max(
                0,
                currentUnlocked - _poolSize + _additionallyStatVisibleCount
            );
            var totalToCreate = Mathf.Max(
                _poolSize,
                currentUnlocked + _additionallyStatVisibleCount
            );

            for (var i = startIndex; i < totalToCreate; i++)
            {
                var statIndex = i % _statsData.Stats.Count;
                var statBar = Instantiate(_statBarPrefab, _statContainer);

                statBar.Init(this, _statsData.Stats[statIndex], i);
                _statBars.Add(statBar);
            }

            RefreshUI();
            EnsureLayout();
        }

        private void InitializeAdditions()
        {
            if (!_additionData || !_additionBarPrefab || !_additionsContainer)
            {
                Debug.LogWarning(
                    "[GeneticSystem] Не назначены зависимости Additions!",
                    this
                );
                return;
            }

            if (_additionBars.Count > 0)
                return;

            for (var i = 0; i < _initialAdditionsCount; i++)
            {
                if (!TryCreateAddition())
                    break;
            }

            if (_statUpgradesPerAddition > 0)
            {
                _lastAdditionUpgradeCount =
                    YG2.saves.IdSavedStatCount -
                    YG2.saves.IdSavedStatCount % _statUpgradesPerAddition;
            }

            EnsureLayout();
        }

        private void TryAddAddition()
        {
            if (_statUpgradesPerAddition <= 0)
                return;

            var upgradesCount = YG2.saves.IdSavedStatCount;

            if (upgradesCount <= 0 || upgradesCount % _statUpgradesPerAddition != 0)
                return;

            if (_lastAdditionUpgradeCount == upgradesCount)
                return;

            if (!TryCreateAddition()) return;
            _lastAdditionUpgradeCount = upgradesCount;
            EnsureLayout();
        }

        private bool TryCreateAddition()
        {
            if (!_additionData || !_additionBarPrefab || !_additionsContainer)
            {
                Debug.LogWarning(
                    "[GeneticSystem] Не назначены зависимости Additions!",
                    this
                );
                return false;
            }

            if (!_additionData.TryGetRandomReward(
                    out var definition,
                    out var count))
            {
                Debug.LogWarning(
                    "[GeneticSystem] Не удалось получить случайную награду Addition!",
                    this
                );
                return false;
            }

            var additionBar = Instantiate(_additionBarPrefab, _additionsContainer);
            additionBar.Init(definition, count);

            _additionBars.Add(additionBar);
            return true;
        }

        private void EnsureVisibleRange()
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
                statBar.Init(this, _statsData.Stats[statIndex], newIndex);
                _statBars.Add(statBar);

                maxIndexInPool = newIndex;
                layoutChanged = true;
            }

            if (layoutChanged)
                EnsureLayout();

            RefreshUI();
        }

        private void EnsureLayout()
        {
            if (_statContainer)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_statContainer);

            if (_additionsContainer)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_additionsContainer);

            if (_scrollRect && _scrollRect.content)
                LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);

            Canvas.ForceUpdateCanvases();
        }

        private void RefreshUI()
        {
            foreach (var bar in _statBars)
            {
                if (bar)
                    bar.UpdateDisplay();
            }
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

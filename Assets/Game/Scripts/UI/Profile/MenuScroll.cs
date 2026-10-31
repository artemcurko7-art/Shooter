using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Profile
{
    public class MenuScroll : MonoBehaviour
    {
        [SerializeField] private int _startPageIndex = 0;
        [SerializeField] private SmoothScroll _scroll;
        [SerializeField] private List<RectTransform> _pages = new();

        [Header("Кнопки")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _avatarsButton;
        [SerializeField] private Button _statsButton;

        private int _currentPageIndex;

        private void Awake()
        {
            if (_pages.Count > 0)
                OpenPage(_startPageIndex);
        }

        private void OnEnable()
        {
            _settingsButton.onClick.AddListener(() => OpenPage(0));
            _avatarsButton.onClick.AddListener(() => OpenPage(1));
            _statsButton.onClick.AddListener(() => OpenPage(2));
        }

        private void OnDisable()
        {
            _settingsButton.onClick.RemoveAllListeners();
            _avatarsButton.onClick.RemoveAllListeners();
            _statsButton.onClick.RemoveAllListeners();
        }

        private void OpenPage(int index)
        {
            if (index < 0 || index >= _pages.Count)
            {
                Debug.LogWarning($"[MenuScroll] Неверный индекс страницы: {index}. Доступно: 0–{_pages.Count - 1}");
                return;
            }

            _currentPageIndex = index;
            ScrollToPage(_currentPageIndex);
        }

        private void ScrollToPage(int pageIndex)
        {
            if (!_scroll || !_scroll.GetScrollRect)
                return;

            var scrollRect = _scroll.GetScrollRect;
            var content = scrollRect.content;
            var viewport = scrollRect.viewport;

            if (!content || !viewport || pageIndex >= _pages.Count || _pages[pageIndex] == null)
                return;

            var target = _pages[pageIndex];

            var targetCenterLocal = content.InverseTransformPoint(
                target.TransformPoint(target.rect.center));

            var viewportCenterLocal = content.InverseTransformPoint(
                viewport.TransformPoint(viewport.rect.center));

            var targetX = content.anchoredPosition.x + viewportCenterLocal.x - targetCenterLocal.x;

            _scroll.ScrollToX(targetX);
        }
    }
}
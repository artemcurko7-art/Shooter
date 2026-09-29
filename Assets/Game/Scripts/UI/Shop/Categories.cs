using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class Categories : MonoBehaviour
    {
        [SerializeField] private List<CategoryButton> _buttons;
        [SerializeField] private SmoothScroll _categoryScroll;
        [SerializeField] private SmoothScroll _shopScroll;
        [SerializeField] private float _shopScrollTopOffset;

        private void Start()
        {
            if (_buttons.Count == 0) return;
            Refresh(_buttons[0]);
        }

        public void Refresh(CategoryButton categoryButton)
        {
            foreach (var button in _buttons)
            {
                var isActive = button == categoryButton;
                button.SetActive(isActive);
                button.UpdateVisual();
            }

            MoveCategory(categoryButton.transform as RectTransform);
        }

        public void GetTarget(RectTransform target)
        {
            if (!target || !_shopScroll)
                return;

            var scrollRect = GetScrollRect(_shopScroll);

            if (!scrollRect || !scrollRect.content || !scrollRect.viewport) return;

            var content = scrollRect.content;

            EnsureLayout(content);

            var targetTopLocalY = target.anchoredPosition.y + target.rect.height * (1f - target.pivot.y);

            var targetY = -targetTopLocalY;
            targetY += _shopScrollTopOffset;

            var contentHeight = content.rect.height;
            var viewportHeight = scrollRect.viewport.rect.height;

            var maxScroll = Mathf.Max(0f, contentHeight - viewportHeight);

            targetY = Mathf.Clamp(targetY, 0f, maxScroll);

            _shopScroll.ScrollToY(targetY);
        }

        private void MoveCategory(RectTransform target)
        {
            if (!target || !_categoryScroll) return;

            var scrollRect = GetScrollRect(_categoryScroll);

            if (!scrollRect || !scrollRect.content || !scrollRect.viewport) return;

            var content = scrollRect.content;
            var viewport = scrollRect.viewport;

            EnsureLayout(content);

            var corners = new Vector3[4];
            target.GetWorldCorners(corners);

            var targetCenter = (corners[0] + corners[2]) * 0.5f;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewport, RectTransformUtility.WorldToScreenPoint(null, targetCenter),
                    null, out var viewportPoint))
            {
                return;
            }

            var deltaX = viewportPoint.x - viewport.rect.center.x;
            if (Mathf.Abs(deltaX) < 1f) return;

            var targetX = content.anchoredPosition.x - deltaX;
            var contentWidth = content.rect.width;
            var viewportWidth = viewport.rect.width;

            if (contentWidth > viewportWidth)
            {
                var minX = -(contentWidth - viewportWidth);
                const float maxX = 0f;
                targetX = Mathf.Clamp(targetX, minX, maxX);
            }

            _categoryScroll.ScrollToX(targetX);
        }

        private static ScrollRect GetScrollRect(SmoothScroll smoothScroll)
        {
            return smoothScroll.GetComponent<ScrollRect>();
        }

        private static void EnsureLayout(RectTransform content)
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            Canvas.ForceUpdateCanvases();
        }
    }
}
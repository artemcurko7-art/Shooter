using System.Collections.Generic;
using System.Linq;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<CategoryButton> _buttons;
        [SerializeField] private ShopPreview _preview;
        [SerializeField] private SmoothScroll _categoryScroll;
        [SerializeField] private SmoothScroll _shopScroll;
        [SerializeField] private float _shopScrollTopOffset;

        public bool IsPreviewAnimating => _preview.IsEffectAnimating;

        private void Start()
        {
            if (_buttons == null || _buttons.Count == 0)
                return;

            foreach (var button in _buttons.Where(button => button))
            {
                RefreshCategory(button);
                return;
            }
        }

        public void RefreshCategory(CategoryButton categoryButton)
        {
            if (categoryButton == null)
                return;

            if (_buttons == null || _buttons.Count == 0)
                return;

            foreach (var button in _buttons)
            {
                if (!button)
                    continue;

                var isActive = button == categoryButton;
                button.SetActive(isActive);
                button.UpdateVisual();
            }

            if (!categoryButton.transform)
                return;

            var target = categoryButton.transform as RectTransform;

            if (target == null)
                return;

            HorizontalScrollMove(target);
        }

        public void OpenPreview(BuyItemData.BuyItem buyItem, Vector3 startPosition)
        {
            Debug.Log("Shop.OpenPreview()", this);

            if (_preview == null)
            {
                Debug.LogWarning("Shop: ShopPreview is NULL.", this);
                return;
            }

            if (buyItem == null)
            {
                Debug.LogWarning("Shop: BuyItem is NULL.", this);
                return;
            }

            Debug.Log("Shop: calling ShopPreview.Open()", _preview);

            _preview.Open(buyItem, startPosition);
        }

        public void VerticalScrollMove(RectTransform target)
        {
            if (target == null)
            {
                Debug.LogWarning($"{nameof(Shop)}: Vertical scroll target is null.", this);
                return;
            }

            if (_shopScroll == null)
            {
                Debug.LogWarning($"{nameof(Shop)}: Shop SmoothScroll is not assigned.", this);
                return;
            }

            var scrollRect = GetScrollRect(_shopScroll);

            if (scrollRect == null)
                return;

            if (scrollRect.content == null)
            {
                Debug.LogWarning($"{nameof(Shop)}: Shop ScrollRect content is not assigned.", this);
                return;
            }

            if (scrollRect.viewport == null)
            {
                Debug.LogWarning($"{nameof(Shop)}: Shop ScrollRect viewport is not assigned.", this);
                return;
            }

            var content = scrollRect.content;

            EnsureLayout(content);

            var targetTopLocalY = target.anchoredPosition.y +
                                  target.rect.height * (1f - target.pivot.y);

            var targetY = -targetTopLocalY;
            targetY += _shopScrollTopOffset;

            var contentHeight = content.rect.height;
            var viewportHeight = scrollRect.viewport.rect.height;

            var maxScroll = Mathf.Max(0f, contentHeight - viewportHeight);

            targetY = Mathf.Clamp(targetY, 0f, maxScroll);

            _shopScroll.ScrollToY(targetY);
        }

        private void HorizontalScrollMove(RectTransform target)
        {
            if (target == null)
                return;

            if (_categoryScroll == null)
            {
                Debug.LogWarning(
                    $"{nameof(Shop)}: Category SmoothScroll is not assigned.",
                    this);

                return;
            }

            var scrollRect = GetScrollRect(_categoryScroll);

            if (scrollRect == null)
                return;

            if (scrollRect.content == null)
            {
                Debug.LogWarning(
                    $"{nameof(Shop)}: Category ScrollRect content is not assigned.",
                    this);

                return;
            }

            if (scrollRect.viewport == null)
            {
                Debug.LogWarning(
                    $"{nameof(Shop)}: Category ScrollRect viewport is not assigned.",
                    this);

                return;
            }

            var content = scrollRect.content;
            var viewport = scrollRect.viewport;

            EnsureLayout(content);

            var corners = new Vector3[4];
            target.GetWorldCorners(corners);

            var targetCenter = (corners[0] + corners[2]) * 0.5f;

            var screenPoint = RectTransformUtility.WorldToScreenPoint(null, targetCenter);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewport,
                    screenPoint,
                    null,
                    out var viewportPoint))
            {
                return;
            }

            var deltaX = viewportPoint.x - viewport.rect.center.x;

            if (Mathf.Abs(deltaX) < 1f)
                return;

            var targetX = content.anchoredPosition.x - deltaX;

            var contentWidth = content.rect.width;
            var viewportWidth = viewport.rect.width;

            if (contentWidth > viewportWidth)
            {
                var minX = -(contentWidth - viewportWidth);
                const float maxX = 0f;

                targetX = Mathf.Clamp(targetX, minX, maxX);
            }
            else
            {
                targetX = 0f;
            }

            _categoryScroll.ScrollToX(targetX);
        }

        private static ScrollRect GetScrollRect(SmoothScroll smoothScroll)
        {
            if (smoothScroll == null)
                return null;

            return smoothScroll.GetScrollRect;
        }

        private static void EnsureLayout(RectTransform content)
        {
            if (content == null)
                return;

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            Canvas.ForceUpdateCanvases();
        }
    }
}
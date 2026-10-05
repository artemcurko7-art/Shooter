using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Shop
{
    public class BuyItemBlock : MonoBehaviour
    {
        private const int MAX_ITEMS_COUNT = 6;

        [SerializeField] private Shop _shop;
        [SerializeField] private RectTransform _content;
        [SerializeField] private BuyItemBar _prefab;
        [SerializeField] private BuyItemData _data;

        private readonly List<BuyItemBar> _bars = new();

        private void Start()
        {
            InitializeBars();
        }

        private void InitializeBars()
        {
            if (_shop == null)
            {
                Debug.LogWarning($"{nameof(BuyItemBlock)}: Shop is not assigned.", this);
                return;
            }

            if (_content == null)
            {
                Debug.LogWarning($"{nameof(BuyItemBlock)}: Content is not assigned.", this);
                return;
            }

            if (_prefab == null)
            {
                Debug.LogWarning($"{nameof(BuyItemBlock)}: BuyItemBar prefab is not assigned.", this);
                return;
            }

            if (_data == null)
            {
                Debug.LogWarning($"{nameof(BuyItemBlock)}: BuyItemData is not assigned.", this);
                return;
            }

            if (_data.items == null || _data.items.Count == 0)
                return;

            if (_bars.Count > 0)
                return;

            var itemsCount = Mathf.Min(MAX_ITEMS_COUNT, _data.items.Count);

            for (var i = 0; i < itemsCount; i++)
            {
                var buyItem = _data.items[i];

                if (buyItem == null)
                {
                    Debug.LogWarning(
                        $"{nameof(BuyItemBlock)}: Item at index {i} is null.",
                        this);

                    continue;
                }

                var bar = Instantiate(_prefab, _content);

                if (bar == null)
                {
                    Debug.LogWarning(
                        $"{nameof(BuyItemBlock)}: Failed to instantiate BuyItemBar at index {i}.",
                        this);

                    continue;
                }

                bar.Init(_shop, buyItem, false);
                _bars.Add(bar);
            }
        }
    }
}
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

        public void InitializeBars(out List<Transform> items)
        {
            items = new List<Transform>();

            if (!_shop) return;

            if (!_content) return;

            if (!_prefab) return;

            if (!_data) return;

            if (_data.items == null || _data.items.Count == 0) return;

            if (_bars.Count > 0) return;

            var itemsCount = Mathf.Min(MAX_ITEMS_COUNT, _data.items.Count);

            for (var i = 0; i < itemsCount; i++)
            {
                var buyItem = _data.items[i];

                if (buyItem == null)
                    continue;

                var bar = Instantiate(_prefab, _content);
                bar.Init(_shop, buyItem, false);

                _bars.Add(bar);
                items.Add(bar.transform);
            }
        }
    }
}
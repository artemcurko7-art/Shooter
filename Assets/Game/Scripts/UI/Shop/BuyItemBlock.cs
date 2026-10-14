using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class BuyItemBlock : MonoBehaviour
    {
        [Header("Настройки лимита")]
        [Tooltip("Если включено — будет применён лимит MaxItemsCount.")]
        [SerializeField] private bool _useMaxItemsLimit = false;

        [Tooltip("Максимальное количество элементов.")]
        [SerializeField] private int _maxItemsCount = 6;

        [SerializeField] private Shop _shop;
        [SerializeField] private RectTransform _content;
        [SerializeField] private BuyItemBar _prefab;
        [SerializeField] private BuyItemData _data;

        private readonly List<BuyItemBar> _bars = new();

        public void InitializeBars(out List<Transform> items)
        {
            items = new List<Transform>();

            if (!_shop || !_content || !_prefab || !_data)
            {
                Debug.LogWarning("[BuyItemBlock] Missing required references!", this);
                return;
            }

            if (_data.Items == null || _data.Items.Count == 0)
                return;

            if (_bars.Count > 0)
            {
                items.AddRange(_bars.Select(bar => bar.transform));
                LayoutRebuilder.MarkLayoutForRebuild(_content);
                return;
            }

            var itemsCountToCreate = _useMaxItemsLimit
                ? Mathf.Min(_maxItemsCount, _data.Items.Count)
                : _data.Items.Count;

            for (var i = 0; i < itemsCountToCreate; i++)
            {
                var buyItem = _data.Items[i];

                if (buyItem == null || !_data.GetDefinition(buyItem))
                    continue;

                var bar = Instantiate(_prefab, _content);
                bar.Init(_shop, _data, buyItem, false);

                _bars.Add(bar);
                items.Add(bar.transform);
            }

            ForceUpdateLayout();
        }

        private void ForceUpdateLayout()
        {
            if (!_content)
                return;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
            Canvas.ForceUpdateCanvases();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Shop
{
    public class BuyItemBlock : MonoBehaviour
    {
        private const int MAX_ITEMS_COUNT = 6;

        [SerializeField] private RectTransform _content;
        [SerializeField] private BuyItemBar _prefab;
        [SerializeField] private BuyItemData _data;

        private List<BuyItemBar> _bars = new();

        private void Start()
        {
            InitializeBars();
        }

        private void InitializeBars()
        {
            for (var i = 0; i < MAX_ITEMS_COUNT && i < _data.items.Count; i++)
            {
                var bar = Instantiate(_prefab, _content);
                bar.Init(_data.items[i], false);
                _bars.Add(bar);
            }
        }
    }
}
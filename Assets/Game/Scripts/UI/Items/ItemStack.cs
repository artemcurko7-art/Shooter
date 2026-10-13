using System;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField] private ItemDefinition _item;
        [SerializeField] private int _count = 1;

        public ItemDefinition Item => _item;
        public int Count => _count;
    }
}
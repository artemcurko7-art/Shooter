using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.WheelFortune
{
    [CreateAssetMenu(fileName = "BoxesData", menuName = "Game/Wheel Fortune/BoxesData")]
    public class BoxesData : ScriptableObject
    {
        [SerializeField] private List<Box> _boxes = new();

        public IReadOnlyList<Box> Boxes => _boxes;

        [Serializable]
        public class Box
        {
            public LocalizedName _nameTranslations;
            public RarityType _rarityType;
            public int boxCount;
            public int keyCount;
            public ItemDefinition box;
            public ItemDefinition key;
            public List<Loot> loots = new();

            [Serializable]
            public class Loot
            {
                public Reward reward;
                [Min(0f)] public float chance = 1f;
            }
        }
    }
}
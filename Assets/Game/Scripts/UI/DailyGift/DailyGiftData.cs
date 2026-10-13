using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.DailyGift
{
    [CreateAssetMenu(fileName = "DailyGiftData", menuName = "Game/Daily Gifts")]
    public class DailyGiftData : ScriptableObject
    {
        [SerializeField] private List<DailyGift> _gifts = new();

        public List<DailyGift> Gifts => _gifts;

        [Serializable]
        public class DailyGift
        {
            public Reward Reward;
        }
    }
}
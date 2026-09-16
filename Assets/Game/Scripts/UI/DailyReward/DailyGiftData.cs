using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.DailyReward
{
    [CreateAssetMenu(fileName = "DailyGiftData", menuName = "Data/DailyGiftData")]
    public class DailyGiftData : ScriptableObject
    {
        public List<DailyGift> Gifts = new();

        [Serializable]
        public class DailyGift
        {
            public Sprite icon;
            public int count;
        }
    }
}
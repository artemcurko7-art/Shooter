using System.Collections.Generic;
using Game.Scripts.UI.DailyGift;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        //Генетика
        public int IdSavedStatCount = 0;

        public float AttackStrength = 1f;
        public float CriticalDamage = 1f;
        public float Armor = 1f;
        public float MovementSpeed = 1f;
        public float ViewRange = 1f;

        //Ежедневные подарки
        public List<int> TakenDailyGiftDays = new();
        public List<int> TakenDailyGiftBoxes = new();
        public int TotalCollectedGifts;
        public int DailyGiftWeek;
        public long GiftEndTime;
    }
}
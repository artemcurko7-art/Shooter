using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        // --- Генетика ---
        public int IdSavedStatCount = 0;
        public float AttackStrength = 1f;
        public float CriticalDamage = 1f;
        public float Armor = 1f;
        public float MovementSpeed = 1f;
        public float ViewRange = 1f;

        // --- Ежедневные подарки ---
        public List<int> TakenDailyGiftDays = new();
        public List<int> TakenDailyGiftBoxes = new();
        public int TotalCollectedGifts;
        public int DailyGiftWeek;
        public long GiftEndTime;

        // --- Энергия ---
        public int Energy = 0;
        public string NextEnergyRestoreTime = string.Empty;

        // --- Аватары ---
        public List<string> OpenedAvatarIds = new() { "1", "2" };
        public string SelectedAvatarId = "1";
        
        // --- Настройки ---
        public bool Music = true;
        public bool Sound = true;
        public bool Animation = true;
    }
}
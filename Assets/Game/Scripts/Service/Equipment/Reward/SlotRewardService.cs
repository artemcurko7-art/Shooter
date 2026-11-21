using Game.Scripts.Common.Classes.Rarity;
using Game.Scripts.Equipment.EquipmentContext.Data;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.Reward
{
    public abstract class SlotRewardService
    {
        public SlotRewardService(IRarityData rarityData, Transform container)
        {
            RarityData = rarityData;
            Container = container;
        }
        
        protected IRarityData RarityData { get; }
        protected Transform Container { get; }

        public abstract void Execute();
    }
}
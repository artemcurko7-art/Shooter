using Game.Scripts.Equipment.EquipmentContext.Data;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.Reward
{
    public abstract class SlotRewardService
    {
        public SlotRewardService(RarityData rarityData, Transform container)
        {
            RarityData = rarityData;
            Container = container;
        }
        
        protected RarityData RarityData { get; }
        protected Transform Container { get; }

        public abstract void Execute();
    }
}
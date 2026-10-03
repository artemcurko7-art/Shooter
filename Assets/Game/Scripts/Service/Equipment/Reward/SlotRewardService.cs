using Game.Scripts.Equipment.EquipmentContext.Data;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.Reward
{
    public abstract class SlotRewardService
    {
        public SlotRewardService(RarityEquipmentData rarityData, Transform container)
        {
            RarityData = rarityData;
            Container = container;
        }
        
        protected RarityEquipmentData RarityData { get; }
        protected Transform Container { get; }

        public abstract void Execute();
    }
}
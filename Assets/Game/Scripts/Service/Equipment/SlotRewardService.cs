using Game.Scripts.Equipment.EquipmentContext.Data;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public abstract class SlotRewardService<TData, TFactory>
    {
        public SlotRewardService(RarityEquipmentData rarityData, TData data, TFactory factory, Transform container)
        {
            Data = data;
            Factory = factory;
            RarityData = rarityData;
            Container = container;
        }
        
        protected RarityEquipmentData RarityData { get; }
        protected TData Data { get; }
        protected TFactory Factory { get; }
        protected Transform Container { get; }
        
        public abstract void Execute();
    }
}
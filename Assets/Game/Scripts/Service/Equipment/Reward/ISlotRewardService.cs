using System;
using Game.Scripts.Equipment;

namespace Game.Scripts.Service.Equipment.Reward
{
    public interface ISlotRewardService<out TSlot> where TSlot : Slot
    {
        public event Action<TSlot> Rewarded;
    }
}
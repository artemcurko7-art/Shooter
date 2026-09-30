using System;
using Game.Scripts.Equipment.EquipmentContext;

namespace Game.Scripts.Service.Equipment.EquipmentContext
{
    public interface IEquipmentService
    {
        public event Action<EquipmentSlot> Added;
    }
}
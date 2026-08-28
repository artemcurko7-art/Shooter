using System;
using System.Collections.Generic;
using Game.Scripts.Equipment;

namespace Game.Scripts.Service.Equipment
{
    public interface IEquipmentService
    {
        public event Action<Slot> Added;
        public IReadOnlyList<Slot> Slots { get; }
    }
}
using System;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public interface ITabService<T>
    {
        event Action<bool> TabOpened;
        void DisableTab();
    }
}
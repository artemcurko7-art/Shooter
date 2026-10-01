using System;

namespace Game.Scripts.Equipment
{
    public interface ITabService<T>
    {
        event Action<bool> TabOpened;
        void DisableTab();
    }
}
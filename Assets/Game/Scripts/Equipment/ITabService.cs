using System;

namespace Game.Scripts.Equipment
{
    public interface ITabService
    {
        event Action<bool> TabOpened;
        void DisableTab();
    }
}
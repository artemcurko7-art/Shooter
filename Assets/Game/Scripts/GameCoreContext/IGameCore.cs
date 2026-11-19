using System;

namespace Game.Scripts.GameCoreContext
{
    public interface IGameCore
    {
        public event Action Paused;
        public event Action Resumed;
        public event Action Ended;
    }
}
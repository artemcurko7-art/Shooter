using System;

namespace Game.Scripts.GameCoreContext
{
    public class GameCore : IGameCore
    {
        public event Action Paused;
        public event Action Resumed;
        public event Action Ended;

        public void OnPaused()
        {
            Paused?.Invoke();
        }

        public void OnResumed()
        {
            Resumed?.Invoke();
        }

        public void OnEnded()
        {
            Ended?.Invoke();
        }
    }
}
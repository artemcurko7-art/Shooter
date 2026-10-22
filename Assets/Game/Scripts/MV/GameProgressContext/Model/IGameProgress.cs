using System;

namespace Game.Scripts.MV.GameProgressContext
{
    public interface IGameProgress
    {
        public event Action<int> ValueChanged;
        public int MaxValue { get; }
        public void Update();
    }
}
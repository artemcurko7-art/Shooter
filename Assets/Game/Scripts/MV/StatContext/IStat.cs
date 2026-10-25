using System;

namespace Game.Scripts.MV.StatContext
{
    public interface IStat
    {
        public event Action<int> ValueChanged;
        public int MaxValue { get; }
        public void Update();
    }
}
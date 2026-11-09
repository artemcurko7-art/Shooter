using System;
using Game.Scripts.UnitContext;

namespace Game.Scripts.Service.UnitContext
{
    public interface IUnitService
    {
        public event Action<Unit> Added;
        public int MaxCount { get; }
    }
}
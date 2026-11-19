using System;
using Game.Scripts.Common.Interfaces;

namespace Game.Scripts.Attacked
{
    public interface IAttackable
    {
        public event Action<IAttacker> Attacked;
    }
}
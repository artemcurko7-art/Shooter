using System;

namespace Game.Scripts.Attacked
{
    public interface IAttackable
    {
        public event Action Attacked;
    }
}
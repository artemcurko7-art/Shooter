using System;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;

namespace Game.Scripts.Attacked
{
    public interface IAttackable
    {
        public event Action<IAttacker> Attacked;
    }
}
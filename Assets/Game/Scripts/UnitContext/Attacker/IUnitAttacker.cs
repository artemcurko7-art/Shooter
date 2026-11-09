using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public interface IUnitAttacker
    {
        UnitAttackerType Type { get; }
        void Attack(Transform current, int damage, float distance);
    }
}
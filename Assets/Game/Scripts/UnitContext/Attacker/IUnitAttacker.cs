using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public interface IUnitAttacker
    {
        void Attack(Transform current);
    }
}
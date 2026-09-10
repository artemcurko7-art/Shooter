using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public interface IUnitAttacker
    {
        UnitAttackerType Type { get; }
        void Attack(IDamageable damageable, Transform current, int damage);
    }
}
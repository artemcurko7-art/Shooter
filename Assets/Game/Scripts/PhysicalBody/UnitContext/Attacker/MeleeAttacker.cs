using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class MeleeAttacker : IUnitAttacker
    {
        public MeleeAttacker()
        {
            Type = UnitAttackerType.MeleeAttacker;
        }

        public UnitAttackerType Type { get; }

        public void Attack(IDamagable damagable, Transform current, int damage)
        {
            Debug.Log("Melee Attacker");
        }
    }
}
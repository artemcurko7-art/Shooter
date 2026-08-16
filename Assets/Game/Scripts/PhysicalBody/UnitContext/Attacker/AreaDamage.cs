using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class AreaDamage : IUnitAttacker
    {
        public AreaDamage()
        {
            Type = UnitAttackerType.AreaDamage;
        }

        public UnitAttackerType Type { get; }

        public void Attack(IDamagable damagable, Transform current, int damage)
        {
            Debug.Log($"Area Damage");
        }
    }
}

using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class MeleeAttacker : IUnitAttacker
    {
        public MeleeAttacker()
        {
            Type = UnitAttackerType.Melee;
        }

        public UnitAttackerType Type { get; }

        public void Attack(Transform current, int damage, float distance)
        {
            Ray ray = new Ray(current.position, current.forward);
            
            if (Physics.SphereCast(ray.origin, 0.5f, ray.direction, out var hit, distance))
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(damage);
            
            Debug.Log("Melee Attacker");
        }
    }
}
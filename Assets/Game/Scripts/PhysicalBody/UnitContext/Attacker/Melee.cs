using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class Melee : IUnitAttacker
    {
        private const float Distance = 5;
        
        public Melee()
        {
            Type = UnitAttackerType.Melee;
        }

        public UnitAttackerType Type { get; }

        public void Attack(Transform current, int damage)
        {
            Ray ray = new Ray(current.position, current.forward);
            
            if (Physics.Raycast(ray, out var hit, Distance))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
            
            Debug.Log("Melee Attacker");
        }
    }
}
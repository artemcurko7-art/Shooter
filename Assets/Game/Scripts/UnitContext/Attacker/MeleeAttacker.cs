using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class MeleeAttacker : IUnitAttacker
    {
        private readonly Stat _damage;
        private readonly Stat _distance;
        private readonly Stat _radius;
        
        public MeleeAttacker(Stat damage, Stat distance, Stat radius)
        {
            _damage = damage;
            _distance = distance;
            _radius = radius;
        }

        public UnitAttackerType Type { get; }

        public void Attack(Transform current)
        {
            Ray ray = new Ray(current.position, current.forward);
            
            if (Physics.SphereCast(ray.origin, _radius.Value, ray.direction, out var hit, _distance.Value))
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_radius.Value);
            
            Debug.Log("Melee Attacker");
        }
    }
}
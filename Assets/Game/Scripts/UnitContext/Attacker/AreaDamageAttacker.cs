using System;
using Game.Scripts.Damagable;
using Game.Scripts.HitImpacted;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class AreaDamageAttacker : IUnitAttacker
    {
        private readonly Stat _damage;
        private readonly Stat _radius;
        private readonly LayerMask _layerMask;
        private readonly Collider[] _colliders = new Collider[8];
        
        public event Action<HitContext> HitImpacted;
        
        public AreaDamageAttacker(Stat damage, Stat radius)
        {
            _damage = damage;
            _radius = radius;
            
            _layerMask = LayerMask.GetMask(nameof(Player));
        }

        public void Attack(Transform current)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(current.position, _radius.Value, _colliders, _layerMask);
            
            for (int i = 0; i < hitCount; i++)
            {
                Vector3 targetPosition = _colliders[i].ClosestPoint(current.position);
                Vector3 direction = (targetPosition - current.position).normalized;
                float distance = Vector3.Distance(current.position, targetPosition);

                if (Physics.Raycast(current.position, direction, out RaycastHit hit, distance, _layerMask))
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(_damage.Value);
                        HitImpacted?.Invoke(new HitContext(new ShotContext(null, _damage.Value, false), hit));
                    }
                }
            }
            
            Debug.Log($"Area Damage");
        }
    }
}

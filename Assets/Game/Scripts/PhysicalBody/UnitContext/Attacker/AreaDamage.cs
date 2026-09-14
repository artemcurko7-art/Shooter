using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class AreaDamage : IUnitAttacker
    {
        private readonly LayerMask _layerMask;
        private readonly Collider[] _colliders = new Collider[8];
        
        public AreaDamage()
        {
            Type = UnitAttackerType.AreaDamage;
            _layerMask = LayerMask.GetMask(nameof(Player));
        }

        public UnitAttackerType Type { get; }

        public void Attack(Transform current, int damage)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(current.position, 5, _colliders, _layerMask);
            
            for (int i = 0; i < hitCount; i++)
                if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(damage);
            
            Debug.Log($"Area Damage");
        }
    }
}

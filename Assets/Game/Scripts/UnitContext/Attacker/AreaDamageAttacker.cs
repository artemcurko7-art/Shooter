using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class AreaDamageAttacker : IUnitAttacker
    {
        private readonly Stat _damage;
        private readonly Stat _radius;
        private readonly LayerMask _layerMask;
        private readonly Collider[] _colliders = new Collider[8];
        
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
                if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage.Value);
            
            Debug.Log($"Area Damage");
        }
    }
}

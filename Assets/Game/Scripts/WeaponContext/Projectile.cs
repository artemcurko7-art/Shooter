using System;
using System.Collections;
using Game.Scripts.PhysicalBody;
using Game.Scripts.Damagable;
using Game.Scripts.HitImpacted;
using Game.Scripts.PhysicalBody.UnitContext;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Projectile : PhysicalBody<Projectile>, IImpactReceiver
    {
        private LayerMask _layerMask;
        private Vector3 _direction;
        private float _radius;
        private int _damage;
        private float _speed;
        
        public event Action<Projectile> Released;
        public event Action<RaycastHit> HitImpacted;

        private void Start()
        {
            _layerMask = LayerMask.GetMask(nameof(Unit));
            StartCoroutine(StartReleased());
        }

        private void Update()
        {
            float moveDistance = _speed * Time.deltaTime;
            
            if (Physics.SphereCast(transform.position, transform.localScale.x * _radius, _direction, out var hit, moveDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                    HitImpacted?.Invoke(hit);
                    Released?.Invoke(this);
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, moveDistance);
        }
        
        public void Initialize(Vector3 position, Vector3 direction, float radius, int damage, float speed)
        {
            transform.position = position;
            _direction = direction;
            _damage = damage;
            _radius = radius;
            _speed = speed;
        }
        
        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, transform.localScale.x * _radius);
        // }

        private IEnumerator StartReleased()
        {
            yield return new WaitForSeconds(10);
            
            Released?.Invoke(this);
        }
    }
}
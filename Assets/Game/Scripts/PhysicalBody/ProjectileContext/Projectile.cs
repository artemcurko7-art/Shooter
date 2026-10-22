using System;
using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.HitImpacted;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.ProjectileContext
{
    public class Projectile : PhysicalBody<Projectile>, IImpactReceiver
    {
        private ShotContext _shotContext;
        private LayerMask _layerMask;
        private Vector3 _direction;
        private float _radius;
        private int _damage;
        private float _speed;
        
        public event Action<HitContext> HitImpacted;
        public event Action<Projectile> Released;

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
                    HitImpacted?.Invoke(new HitContext(_shotContext, hit));
                    Released?.Invoke(this);

                    return;
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, moveDistance);
        }
        
        public void Initialize(ShotContext shotContext, Vector3 position, Vector3 direction, int damage, float radius, float speed)
        {
            _shotContext = shotContext;
            transform.position = position;
            _direction = direction;
            _damage = damage;
            _radius = radius;
            _speed = speed;
        }

        private IEnumerator StartReleased()
        {
            yield return new WaitForSeconds(10);
            
            Released?.Invoke(this);
        }
    }
}
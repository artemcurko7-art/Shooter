using System;
using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Bullet : MonoBehaviour
    {
        private LayerMask _layerMask;
        private Vector3 _direction;
        private int _damage;
        private float _speed;

        private void Start()
        {
            _layerMask = LayerMask.GetMask(nameof(Unit));
            StartCoroutine(StartDestroyed());
        }

        private void Update()
        {
            float moveDistance = _speed * Time.deltaTime;
            
            if (Physics.SphereCast(transform.position, 0.5f, _direction, out var hit, moveDistance, _layerMask)) // radius = transform.scale * _radius
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                    Debug.Log("Нанесение урона");
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, moveDistance);
        }
        
        public void Initialize(Vector3 direction, int damage, float speed)
        {
            _direction = direction.normalized;
            _damage = damage;
            _speed = speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }

        private IEnumerator StartDestroyed()
        {
            yield return new WaitForSeconds(3);
            
            Destroy(gameObject);
        }
    }
}
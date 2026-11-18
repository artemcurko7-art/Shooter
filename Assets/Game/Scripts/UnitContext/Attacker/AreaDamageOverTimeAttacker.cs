using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Damagable;
using Game.Scripts.HitImpacted;
using Game.Scripts.MV.StatContext;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class AreaDamageOverTimeAttacker : IUnitAttacker
    {
        private readonly Stat _damage;
        private readonly Stat _radius;
        private readonly LayerMask _layerMask;
        private readonly Collider[] _colliders = new Collider[8];
        private readonly int _cooldown;
        
        public event Action<HitContext> HitImpacted;
        
        public AreaDamageOverTimeAttacker(LayerMask layerMask, Stat damage, Stat radius, int cooldown)
        {
            _damage = damage;
            _radius = radius;
            _cooldown = cooldown;
            
            _layerMask = layerMask;
        }

        public void Attack(Transform current)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            StartAsync(current, cancellationTokenSource.Token).Forget();
            
            Debug.Log($"Area Damage Over Time");
        }

        private async UniTaskVoid StartAsync(Transform current, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
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
                
                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: token);
                
                if (token.IsCancellationRequested)
                    return;
            }
        }
    }
}
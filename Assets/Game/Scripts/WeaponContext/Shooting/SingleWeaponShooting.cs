using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class SingleWeaponShooting : WeaponShooting, IWeaponShooting
    {
        private const int SecondInMilliseconds = 1000;
        private readonly TrackerUnits _trackerUnits;
        private readonly BulletPool _pool;
        private readonly Transform _transform;
        private readonly float _cooldown;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _canShoot;
        
        public event Action Attacked;
        
        public SingleWeaponShooting(TrackerUnits trackerUnits, BulletPool pool, Transform transform, float cooldown)
        {
            Type = ShootingType.Single;
            _trackerUnits = trackerUnits;
            _pool = pool;
            _transform = transform;
            _cooldown = cooldown;
        }
        
        public ShootingType Type { get; }
        
        public void Subscribe()
        {
            // добавить
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public void StartShooting(Bullet bullet, float radius, int damage, float speed)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartCooldown(_cancellationTokenSource.Token, bullet, radius, damage, speed).Forget();
            
            Debug.Log("Single");
        }
        
        private async UniTaskVoid StartCooldown(CancellationToken token, Bullet bullet, float radius, int damage, float speed)
        {
            _pool.SetPrefab(bullet);
            
            while (token.IsCancellationRequested == false)
            {
                float calculationCooldown = _cooldown * SecondInMilliseconds;
                
                await UniTask.Delay((int)calculationCooldown, cancellationToken: token);
        
                if (token.IsCancellationRequested)
                    return;

                if (_trackerUnits.IsTracker == false)
                    continue;
                
                Attacked?.Invoke();

                var obj = _pool.Get();
                obj.Initialize(_transform.position, _trackerUnits.Direction, radius, damage, speed);
            }
        }
    }
}
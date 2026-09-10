using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.PlayerContext;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Single : WeaponShooting, IWeaponShooting
    {
        private const int SecondInMilliseconds = 1000;
        private readonly TrackerUnits _trackerUnits;
        private readonly float _cooldown;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _canShoot;
        
        public event Action Attacked;
        
        public Single(TrackerUnits trackerUnits, float cooldown)
        {
            Type = ShootingType.Single;
            _trackerUnits = trackerUnits;
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
        
        public void StartShooting(Bullet bullet, Transform transform, int damage, float speed)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartCooldown(_cancellationTokenSource.Token, bullet, transform, damage, speed).Forget();
            
            Debug.Log("Single");
        }
        
        private async UniTaskVoid StartCooldown(CancellationToken token, Bullet bullet, Transform transform, int damage, float speed)
        {
            while (token.IsCancellationRequested == false)
            {
                float calculationCooldown = _cooldown * SecondInMilliseconds;
                
                await UniTask.Delay((int)calculationCooldown, cancellationToken: token);
        
                if (token.IsCancellationRequested)
                    return;

                if (_trackerUnits.IsTracker == false)
                    continue;
                
                Attacked?.Invoke();
                
                var obj = GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
                obj.Initialize(transform.forward, damage, speed);
            }
        }
    }
}